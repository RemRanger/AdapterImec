using System.Collections.Generic;
using System.Net;

namespace AdapterImec.Domain.Exceptions
{
    public class ValidationException : JoinDataException
    {
        public IEnumerable<ValidationError> Errors { get; set; }
        public ValidationException(string message) : base(message)
        {

        }

        public ValidationException(string message, IEnumerable<ValidationError> errors) : base(message)
        {
            Errors = errors;
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public class ValidationError
        {
            public string Property { get; set; }
            public string Error { get; set; }
        }
    }
}
