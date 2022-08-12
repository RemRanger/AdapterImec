using AdapterImec.Domain.ValueObjects;
using MediatR;
using System.Text.Json;

namespace AdapterImec.Application.Messages.Commands.CreateMessage
{

    public class CreateMessageCommand : IRequest<string>
    {
        public JsonDocument Content { get; set; }
        public CompanyId Customer { get; set; }
        public CompanyId Provider { get; set; }
        public string Creator { get; set; }
        public string MessageType { get; set; }

        public static CreateMessageCommand CreateFrom(JsonDocument content, CompanyId customer, CompanyId provider, string creator, string messageType)
        {
            return new CreateMessageCommand()
            {
                Content = content,
                Customer = customer,
                Provider = provider,
                Creator = creator,
                MessageType = messageType
            };
        }
    }
}
