using iPassport.Api.Models;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Company Type Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CompanyTypeController : ControllerBase
    {
        /// <summary>
        /// Company Type service property
        /// </summary>
        private readonly ICompanyTypeService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Company Type service instance</param>
        public CompanyTypeController(ICompanyTypeService service) => _service = service;

        /// <summary>
        /// This API is list Company Type
        /// </summary>
        /// <returns>List of Company Type</returns>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business, RolesModel.Government)]
        public async Task<ActionResult> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }
    }
}
