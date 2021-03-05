using AutoMapper;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _service;

        public CountryController(IMapper mapper, ICountryService countryService)
        {
            _mapper = mapper;
            _service = countryService;
        }

        /// <summary>
        /// Get Country by Id
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>Country</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] Guid countryId)
        {
            var res = await _service.FindById(countryId);
            return Ok(res);
        }

        /// <summary>
        /// Get countries by part of name
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>Country</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("ByName")]
        public async Task<ActionResult> GetByNameInitials([FromQuery] GetByNameInitialsPagedRequest request)
        {
            var res = await _service.GetByNameInitials(_mapper.Map<GetByNameInitialsPagedFilter>(request));

            return Ok(res);
        }
    }
}
