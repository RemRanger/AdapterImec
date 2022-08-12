using AdapterImec.Api.Filters.Models;
using AdapterImec.Api.Models;
using AdapterImec.Application.Extensions;
using AdapterImec.Application.Messages.Commands.CreateMessage;
using AdapterImec.Application.Messages.Queries.GetMessageByLocationId;
using AdapterImec.Domain.Exceptions;
using AdapterImec.Domain.ValueObjects;
using AdapterImec.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdapterImec.Api.Controllers
{
    [Route("api/locations")]
    [ApiController]
    [Authorize]
    public class AdapterImecController : ControllerBase
    {
        private const string MessageType_Imec = "imec";

        private readonly IGetImecRequestsService _requestsService;

        public AdapterImecController(IGetImecRequestsService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpGet("{scheme}/{locationId}/imec")]
        [ProducesResponseType(typeof(List<JsonDocument>), 200)]
        [Authorize(Roles = "data_hub")]
        public async Task<IActionResult> GetMessageByLocationId(string scheme, string locationId, [FromQuery] GetMessageByLocationIdParameters parameters)
        {
            var response = await _requestsService.GetPendingRequestsAsync(parameters.DateTimeStart, parameters.DateTimeEnd);
            return Ok(DatahubResponseModelFactory.Create(response));
        }
    }
}
