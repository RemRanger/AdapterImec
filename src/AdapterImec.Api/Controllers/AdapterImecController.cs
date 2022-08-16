using AdapterImec.Api.Models;
using AdapterImec.Application.Messages.Queries.GetMessageByLocationId;
using AdapterImec.Services;
using AdapterImec.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdapterImec.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class AdapterImecController : ControllerBase
    {
        private const string MessageType_Imec = "imec";

        private readonly IGetImecRequestsService _requestsService;
        private readonly ImecSettings _imecSettings;

        public AdapterImecController(IGetImecRequestsService requestsService, IConfiguration configuration)
        {
            _requestsService = requestsService;
            _imecSettings = new ImecSettings();
            configuration.GetSection("ImecSettings").Bind(_imecSettings);
        }

        [HttpGet("pending-requests/{dataSourceId}")]
        [ProducesResponseType(typeof(List<JsonDocument>), 200)]
        [Authorize(Roles = "data_hub")]
        public async Task<IActionResult> GetMessageByLocationId(string dataSourceId)
        {
            try
            {
                var response = await _requestsService.GetPendingRequestsAsync(_imecSettings, dataSourceId);
                return Ok(DatahubResponseModelFactory.Create(response));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
