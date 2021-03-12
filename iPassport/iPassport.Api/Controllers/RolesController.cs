using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  Roles Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        /// <summary>
        /// Role service property
        /// </summary>
        private readonly IRoleService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Role service instance</param>
        public RolesController(IRoleService service) =>  _service = service;

        /// <summary>
        /// This API Create Role
        /// </summary>
        /// <returns>Role Id</returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Add(string urn)
        {
            var res = await _service.Add(urn);
            return Ok(res);
        }
    }
}
