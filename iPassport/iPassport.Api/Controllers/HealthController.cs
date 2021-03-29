using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  Health Controller
    /// </summary>
    [ApiController]
    [Route("api/test/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Health service property
        /// </summary>
        private readonly IHealthService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="healthService">Health service instance</param>
        public HealthController(IHealthService healthService) => _service = healthService;

        /// <summary>
        /// This API is Test Health Check Api
        /// </summary>
        /// <returns>Operation result</returns>
        /// <response code="200">Server returns Ok</response>
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
        /// his API is list Health Check Tests
        /// </summary>
        /// <returns>List of Health Check</returns>
        /// <response code="200">Server returns Ok</response>
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
