using MediatR;
using System.Xml.Serialization;

namespace AdapterImec.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageRequestBody : IRequest<string>
    {
       public string Content { get; set; }
    }
}
