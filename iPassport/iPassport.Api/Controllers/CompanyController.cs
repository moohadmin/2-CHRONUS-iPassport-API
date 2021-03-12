using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Company Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Company Service Property
        /// </summary>
        private readonly ICompanyService _service;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="service">Company Service Instance</param>
        public CompanyController(IMapper mapper, ICompanyService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for Get paged list of Companies by name.
        /// </summary>
        /// <param name="request">Get Company Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Companies list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByNameParts([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetByNamePartsPagedFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Add Company.
        /// </summary>
        /// <param name="request">Company Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Company Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CompanyCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<CompanyCreateDto>(request));
            return Ok(res);
        }
    }
}
