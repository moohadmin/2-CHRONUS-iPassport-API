using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  HealthController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _service;

        public HealthController(IHealthService healthService) => _service = healthService;

        /// <summary>
        /// This API is Test HealthCheck Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var res = await _service.SetHealthyAsync();
            return Ok(res);
        }

        /// <summary>
        /// his API is list HealthCheck Tests
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }
    }
}
