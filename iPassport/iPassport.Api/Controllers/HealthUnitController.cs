using AutoMapper;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Api.Models.Requests.Shared;
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
    /// <summary>
    /// Health Unit Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HealthUnitController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///  Health Unit Service Property
        /// </summary>
        private readonly IHealthUnitService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="healthUnitService">Health Unit Service Instance</param>
        public HealthUnitController(IMapper mapper, IHealthUnitService healthUnitService)
        {
            _mapper = mapper;
            _service = healthUnitService;
        }

        /// <summary>
        /// This API is responsible for Get paged list of Health Unit by name.
        /// </summary>
        /// <param name="request">Get Health Unit Paged Request</param>
        /// <returns>Heailth Unit</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByNameParts([FromQuery] GetHealthUnitPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetHealthUnitPagedFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for add Health Unit.
        /// </summary>
        /// <param name="request">Health Unit Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Health Unit Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] HealthUnitCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<HealthUnitCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get Health Unit by Id.
        /// </summary>
        /// <param name="id">Health Unit Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Health Unit.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }
    }
}
