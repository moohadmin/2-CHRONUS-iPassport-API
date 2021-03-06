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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _service;

        public CompanyController(IMapper mapper, ICompanyService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is get the get Companies by name
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByNameParts([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetByNamePartsPagedFilter>(request));
            return Ok(res);
        }

        ///// <summary>
        ///// This API Add City
        ///// </summary>
        ///// <returns></returns>
        ///// <response code="200">Ok.</response>
        ///// <response code="400">Bussiness Exception</response>
        ///// <response code="500">Due to server problems, it is not possible to get your data now</response>
        //[ProducesResponseType(typeof(ResponseApi), 200)]
        //[ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        //[ProducesResponseType(typeof(ServerErrorResponse), 500)]
        //[HttpPost]
        //public async Task<ActionResult> Add([FromBody] CityCreateRequest request)
        //{
        //    var res = await _service.Add(_mapper.Map<CityCreateDto>(request));
        //    return Ok(res);
        //}
    }
}
