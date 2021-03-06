using AutoMapper;
using iPassport.Api.Models.Requests;
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
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityService _service;

        public CityController(IMapper mapper, ICityService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is get the get Cities by State
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("ByStateAndNameParts")]
        public async Task<ActionResult> GetByStateAndNameParts([FromQuery] GetPagedCitiesByStateAndNamePartsRequest request)
        {
            var res = await _service.FindByStateAndNameParts(_mapper.Map<GetByIdAndNamePartsPagedFilter>(request));
            return Ok(res);
        }

        ///// <summary>
        ///// This API is get the get States by your Country
        ///// </summary>
        ///// <returns></returns>
        ///// <response code="200">Ok.</response>
        ///// <response code="400">Bussiness Exception</response>
        ///// <response code="500">Due to server problems, it is not possible to get your data now</response>
        //[ProducesResponseType(typeof(ResponseApi), 200)]
        //[ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        //[ProducesResponseType(typeof(ServerErrorResponse), 500)]
        //[HttpPost]
        //public async Task<ActionResult> Add([FromForm] StateCreateRequest request)
        //{
        //    var res = await _service.Add(_mapper.Map<StateCreateDto>(request));
        //    return Ok(res);
        //}
    }
}
