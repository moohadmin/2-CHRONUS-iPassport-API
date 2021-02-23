using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PassportController : ControllerBase
    {
        private readonly IPassportService _service;
        public PassportController(IPassportService service) => _service = service;

        /// <summary>
        /// Get user Passport
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
        public async Task<ActionResult> Get()
        {
            var res = await _service.Get(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            return Ok(res);
        }

    }
}
