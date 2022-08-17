using MediatR;

namespace AdapterImec.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageRequestBody : IRequest<string>
    {
        public string Content { get; set; }
    }
}
