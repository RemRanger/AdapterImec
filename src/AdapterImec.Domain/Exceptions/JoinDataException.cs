using System;
using System.Net;

namespace AdapterImec.Domain.Exceptions
{
    public abstract class JoinDataException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        public JoinDataException(string message) : base(message)
        {

        }
    }
}
