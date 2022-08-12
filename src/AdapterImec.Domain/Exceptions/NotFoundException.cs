using System.Net;

namespace AdapterImec.Domain.Exceptions
{
    public class NotFoundException : JoinDataException
    {
        public NotFoundException(string message = "resource not found.") : base(message)
        {

        }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
