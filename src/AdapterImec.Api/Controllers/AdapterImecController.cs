using AdapterImec.Api.Models;
using AdapterImec.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IImecService _imecService;

        public AdapterImecController(IImecService imecService)
        {
            _imecService = imecService;
        }

        [HttpGet("pending-requests/{dataSourceId}")]
        [ProducesResponseType(typeof(List<JsonDocument>), 200)]
        [Authorize(Roles = "data_hub")]
        public async Task<IActionResult> GetMessageByLocationId(string dataSourceId)
        {
            try
            {
                var response = await _imecService.GetPendingRequestsAsync(dataSourceId);
                return Ok(DatahubResponseModelFactory.Create(response));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
