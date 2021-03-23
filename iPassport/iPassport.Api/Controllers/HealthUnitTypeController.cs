using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Health Unit Type Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class HealthUnitTypeController : ControllerBase
    {
        /// <summary>
        /// Health Unit Type service property
        /// </summary>
        private readonly IHealthUnitTypeService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Health Unit Type service instance</param>
        public HealthUnitTypeController(IHealthUnitTypeService service) => _service = service;

        /// <summary>
        /// his API is list Health Unit Type
        /// </summary>
        /// <returns>List of Health Unit Type</returns>
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
