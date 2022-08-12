using AdapterImec.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AdapterImec.Api.Filters.Models
{
    public class JoinDataResponseModel
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// ErrorDateUtc
        /// </summary>
        public DateTime ErrorDateUtc { get; set; }

        public List<Error> Errors { get; set; }

        public class Error {
            public string Message { get; set; }
            public string Property { get; set; }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static JoinDataResponseModel Create(JoinDataException error)
        {
            return new JoinDataResponseModel
            {
                Message = error.Message,
                ErrorDateUtc = DateTime.UtcNow,
                StatusCode = error.StatusCode
            };
        }

        public static JoinDataResponseModel Create(Exception error)
        {
            var exception = error;
            var message = $"{exception.GetType().Name}: {exception.Message}";
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                message += $" | {exception.GetType().Name}: {exception.Message}";
            }

            return new JoinDataResponseModel
            {
                Message = message,
                ErrorDateUtc = DateTime.UtcNow,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        internal static JoinDataResponseModel Create(ValidationException valEx)
        {
            return new JoinDataResponseModel
            {
                Message = valEx.Message,
                ErrorDateUtc = DateTime.UtcNow,
                StatusCode = valEx.StatusCode,
                Errors = valEx.Errors?.Select(x => new Error { Message = x.Error, Property = x.Property }).ToList()
            };
        }

    }
}
