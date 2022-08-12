using AdapterImec.Domain.Abstract;
using AdapterImec.Domain.Entities;
using AdapterImec.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdapterImec.Application.Messages.Queries.GetMessageByLocationId
{
    internal class GetMessageByLocationIdHandler : IRequestHandler<GetMessageByLocationIdQuery, List<JsonDocument>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger _logger;

        public GetMessageByLocationIdHandler(IMessageRepository messageRepository, ILogger<GetMessageByLocationIdHandler> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task<List<JsonDocument>> Handle(GetMessageByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var provider = new CompanyId(request.Parameters.ProviderCompanyScheme, request.Parameters.ProviderCompanyId);

            _logger.LogInformation($"Getting data for Customer {request.CustomerId} ({provider})");

            List<Message> messages = await _messageRepository.GetByCustomerValueAsync(request.CustomerId, request.Parameters.DateTimeStart, request.Parameters.DateTimeEnd, request.Parameters.DateTimeModified, provider, cancellationToken);

            _logger.LogInformation($"Found {messages?.Count ?? 0} for Customer {request.CustomerId} ({provider})");

            var documents = new List<JsonDocument>();
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    documents.Add(JsonDocument.Parse(message.FileContent));
                }
            }

            return documents;
        }
    }
}
