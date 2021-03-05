using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Responses;
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
        /// This API Create User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> Get([FromBody] Guid countryId)
        {
            var res = await _service.FindById(countryId);
            return Ok(res);
        }


    }
}
