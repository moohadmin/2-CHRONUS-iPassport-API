using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PassportController : ControllerBase
    {
        private readonly IPassportService _service;
        private readonly IMapper _mapper;
        public PassportController(IPassportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user Passport
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var res = await _service.Get();
            return Ok(res);
        }

        /// <summary>
        /// Saves Passport Used Acess Dened
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("AccessDenied")]
        public async Task<ActionResult> AcessDenied([FromBody] PassportUseCreateRequest request)
        {
            var res = await _service.AddAccessDenied(_mapper.Map<PassportUseCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// Saves Passport Acess Aproved
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
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
        /// Gets User of passport Details Send
        /// </summary>
        /// <param name="passportDetailsId">Passport Details Id</param>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("User")]
        public async Task<ActionResult> GetPassportUserToValidate(System.Guid passportDetailsId)
        {
            var res = await _service.GetPassportUserToValidate(passportDetailsId);
            return Ok(res);
        }
    }
}
