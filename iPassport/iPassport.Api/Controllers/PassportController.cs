using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Passport Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PassportController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Passport service property
        /// </summary>
        private readonly IPassportService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Passport service instance</param>
        /// <param name="mapper">Auto Mapper instence</param>
        public PassportController(IPassportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// This API is responsible for Get User Passport.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Passport Object.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string imageSize)
        {
            var res = await _service.Get(imageSize);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Deny User Access.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("AccessDenied")]
        public async Task<ActionResult> AccessDenied([FromBody] PassportUseCreateRequest request)
        {
            var res = await _service.AddAccessDenied(_mapper.Map<PassportUseCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Aprove User Access.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("AccessApproved")]
        public async Task<ActionResult> AccessApproved([FromBody] PassportUseCreateRequest request)
        {
            var res = await _service.AddAccessApproved(_mapper.Map<PassportUseCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get the User Passport.
        /// </summary>
        /// <param name="passportDetailsId">Passport Details Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Passport.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("User")]
        public async Task<ActionResult> GetPassportUserToValidate(Guid passportDetailsId)
        {
            var res = await _service.GetPassportUserToValidate(passportDetailsId);
            return Ok(res);
        }
    }
}
