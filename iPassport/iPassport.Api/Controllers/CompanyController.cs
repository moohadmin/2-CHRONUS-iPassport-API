using AutoMapper;
using iPassport.Api.Models;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]
        public async Task<ActionResult> Add([FromBody] CompanyCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<CompanyCreateDto>(request));
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
        /// This API is responsible for Get paged list of Headquarters Companies by name.
        /// </summary>
        /// <param name="request">Get Headquarter Company Paged Request</param>
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

        public async Task<ActionResult> GetHeadquartersCompanies([FromQuery] GetHeadquarterCompanyPagedRequest request)
        {

            var res = new List<HeadquarterCompanyViewModel>() { 
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "94802367000172", Name = "Empresa 1"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "66364363000114", Name = "Empresa 2"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "32412148000120", Name = "Empresa 3"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "88400978000191", Name = "Empresa 4"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "35044227000113", Name = "Empresa 5"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "11397374000109", Name = "Empresa 6"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "37655229000174", Name = "Empresa 7"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "55358824000120", Name = "Empresa 8"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "07178740000161", Name = "Empresa 9"},
                new HeadquarterCompanyViewModel() { Id = Guid.NewGuid(), Cnpj = "29617892000156", Name = "Empresa 10"},
            };
            
            return Ok(new PagedResponseApi(true, "Headquarters Companies", 1, 10, 1, 10, res));
        }
    }
}
