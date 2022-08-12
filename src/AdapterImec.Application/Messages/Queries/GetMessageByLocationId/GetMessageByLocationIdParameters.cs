using Microsoft.AspNetCore.Mvc;
using System;

namespace AdapterImec.Application.Messages.Queries.GetMessageByLocationId
{
    public class GetMessageByLocationIdParameters
    {
        [FromQuery(Name = "start-date-time")]
        public DateTime DateTimeStart { get; set; }

        [FromQuery(Name = "end-date-time")]
        public DateTime DateTimeEnd { get; set; }

        [FromQuery(Name = "modified-since")]
        public DateTime? DateTimeModified { get; set; }

        [FromQuery(Name = "provider-company-scheme")]
        public string ProviderCompanyScheme { get; set; }

        [FromQuery(Name = "provider-company-id")]
        public string ProviderCompanyId { get; set; }
    }
}
