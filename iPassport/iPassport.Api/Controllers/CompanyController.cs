using AutoMapper;
using iPassport.Api.Models;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]

        public async Task<ActionResult> GetByNameParts([FromQuery] GetCompaniesPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetCompaniesPagedFilter>(request));
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
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]
        public async Task<ActionResult> Add([FromBody] CompanyCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<CompanyCreateDto>(request));
            
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Edit Company.
        /// </summary>
        /// <param name="request">Company Edit Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Company Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPut]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]
        public async Task<ActionResult> Edit([FromBody] CompanyEditRequest request)
        {
            var res = await _service.Edit(_mapper.Map<CompanyEditDto>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get Company by Id.
        /// </summary>
        /// <param name="id">Company Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Company.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{id}")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]
        public async Task<ActionResult> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get list of Headquarters Companies by cnpj.
        /// </summary>
        /// <param name="request">Get Headquarter Company Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Headquarters Companies list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Headquarter")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business, RolesModel.Government)]
        public async Task<ActionResult> GetHeadquartersCompanies([FromQuery] GetHeadquarterCompanyRequest request) =>
            Ok(await _service.GetHeadquartersCompanies(_mapper.Map<GetHeadquarterCompanyFilter>(request)));

        /// <summary>
        /// This API Get Company Types
        /// </summary>
        /// <returns>List of Company Type</returns>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Types")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business, RolesModel.Government)]
        public async Task<ActionResult> GetAllTypes()
        {
            var res = await _service.GetAllTypes();
            return Ok(res);
        }

        /// <summary>
        /// This API Get Company Segments by Company Type
        /// </summary>
        /// <param name="id">Parent Id</param>
        /// <param name="request">Page Filter Request</param>
        /// <returns>List of Company Type</returns>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Types/{id}/Segments")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business, RolesModel.Government)]
        public async Task<ActionResult> GetPagedSegmetsByTypeId([FromRoute] Guid id,[FromQuery] PageFilterRequest request)
        {
            var res = await _service.GetSegmetsByTypeId(id, _mapper.Map<PageFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API Get Paged Companies Candidates to be Subsidiaries
        /// </summary>
        /// <param name="id">Parent Id</param>
        /// <param name="request">Page Filter Request</param>
        /// <returns>List of Paged Companies Candidates</returns>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{id}/Subsidiaries/Candidates")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> GetSubsidiariesCandidates([FromRoute] Guid id, [FromQuery]PageFilterRequest request)
        {
            var res = await _service.GetSubsidiariesCandidatesPaged(id, _mapper.Map<PageFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API Associate Companies Subsidiaries
        /// </summary>
        /// <param name="id">Parent Id</param>
        /// <param name="request">Associate Subsidiaries Request</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("{id}/Subsidiaries")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> PostSubsidiaries([FromRoute] Guid id, [FromBody] AssociateSubsidiariesRequest request)
        {
            var dto = _mapper.Map<AssociateSubsidiariesDto>(request);
            dto.ParentId = id;
            
            var res = await _service.AssociateSubsidiaries(dto);
            
            return Ok(res);
        }
    }
}
