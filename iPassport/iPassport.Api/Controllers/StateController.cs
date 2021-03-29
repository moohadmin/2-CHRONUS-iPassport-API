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
    /// <summary>
    /// State Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// State Service Property
        /// </summary>
        private readonly IStateService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="service">State Service Instance</param>
        public StateController(IMapper mapper, IStateService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for Get paged list of States by name and Country.
        /// </summary>
        /// <param name="request">Get State Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged State list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("ByCountry")]
        public async Task<ActionResult> GetByCountryId([FromQuery] GetPagedStatesByCountryRequest request)
        {
            var res = await _service.GetByCountryId(_mapper.Map<GetByIdPagedFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Add State.
        /// </summary>
        /// <param name="request">State Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>State Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] StateCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<StateCreateDto>(request));
            return Ok(res);
        }
    }
}
