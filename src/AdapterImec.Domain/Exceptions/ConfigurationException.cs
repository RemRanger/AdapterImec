using System.Net;

namespace AdapterImec.Domain.Exceptions
{
    public class ConfigurationException : JoinDataException
    {
        public ConfigurationException(string message = "configuration not found") : base(message)
        {
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    }
}
