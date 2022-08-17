using AdapterImec.Domain.Abstract;
using AdapterImec.Domain.Entities;
using AdapterImec.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdapterImec.Application.Messages.Commands.CreateMessage
{
    internal class CreateMessageHandler : IRequestHandler<CreateMessageCommand, string>
    {
        private const string MessageIdElementName = "id";
        private const string MetaElementName = "meta";
        private const string CreatedElementName = "created";

        private readonly IMessageRepository _messageRepository;
        private readonly ILogger _logger;

        public CreateMessageHandler(IMessageRepository messageRepository, ILogger<CreateMessageHandler> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
        {
            var element = command.Content.RootElement;

            var error = await ValidateElement(element, command, cancellationToken);
            if (error == null)
            {
                return await ProcessElement(element, command, cancellationToken);
            }
            else
            {
                throw new ValidationException(error);
            }
        }

        private async Task<string> ValidateElement(JsonElement element, CreateMessageCommand command, CancellationToken cancellationToken)
        {
            string error = null;

            try
            {
                var messageId = GetMessageId(element);
                GetDateCreated(element);

                bool messageExists = await _messageRepository.CheckMessageExistsAsync(messageId, command.Provider.Scheme, command.Provider.Value, cancellationToken);
                if (messageExists)
                {
                    _logger.LogInformation($"MessageId {messageId} from provider {command.Provider} for Customer {command.Customer} already exists");
                    error = $"Message with id = '{messageId}' already exists";
                }
            }
            catch (ValidationException ex)
            {
                error = ex.Message;
            }

            return error;
        }

        private async Task<string> ProcessElement(JsonElement element, CreateMessageCommand command, CancellationToken cancellationToken)
        {
            var messageId = GetMessageId(element);

            Message message = new()
            {
                MessageId = messageId,
                CustomerScheme = command.Customer.Scheme,
                CustomerValue = command.Customer.Value,
                ProvidingCompanyScheme = command.Provider.Scheme,
                ProvidingCompanyValue = command.Provider.Value,
                MessageType = command.MessageType,
                DateCreated = GetDateCreated(element),
                Creator = command.Creator,
                DateReceived = DateTime.UtcNow,
                FileContent = element.ToString()
            };

            await _messageRepository.CreateAsync(message, cancellationToken);

            _logger.LogInformation($"MessageId {messageId} from provider {command.Provider} for Customer {command.Customer} is saved.");

            return messageId;
        }

        private static string GetMessageId(JsonElement element)
        {
            try
            {
                var messageIdElement = element.GetProperty(MessageIdElementName);
                return messageIdElement.GetString();
            }
            catch (KeyNotFoundException)
            {
                throw new ValidationException($"'{MessageIdElementName}' element is missing");
            }
        }

        private static DateTime GetDateCreated(JsonElement element)
        {
            try
            {
                var metaDateElement = element.GetProperty(MetaElementName);
                try
                {
                    var createdElement = metaDateElement.GetProperty(CreatedElementName);
                    var creationDateAsString = createdElement.GetString();
                    return DateTime.TryParse(creationDateAsString, out var creationDate) ? creationDate : DateTime.Now;
                }
                catch (KeyNotFoundException)
                {
                    throw new ValidationException($"'{CreatedElementName}' element is missing in '{MetaElementName}' block");
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ValidationException($"'{MetaElementName}' block is missing");
            }
        }
    }
}
