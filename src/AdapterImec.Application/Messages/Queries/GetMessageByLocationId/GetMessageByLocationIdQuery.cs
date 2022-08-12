using AdapterImec.Domain.ValueObjects;
using MediatR;
using System.Collections.Generic;
using System.Text.Json;

namespace AdapterImec.Application.Messages.Queries.GetMessageByLocationId
{
    public class GetMessageByLocationIdQuery : IRequest<List<JsonDocument>>
    {
        public GetMessageByLocationIdQuery(string scheme, string locationid, GetMessageByLocationIdParameters parameters)
        {
            CustomerId = new CompanyId(scheme, locationid);
            Parameters = parameters;
        }

        public CompanyId CustomerId { get; set; }
        public GetMessageByLocationIdParameters Parameters { get; set; }
    }
}
