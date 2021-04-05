using AutoMapper;
using iPassport.Api.Models;
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
    /// City Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// City Service Property
        /// </summary>
        private readonly ICityService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="service">City Service Instance</param>
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

        /// <summary>
        /// This API add City
        /// </summary>
        /// <param name="request">City Create Request</param>
        /// <returns>Created City id</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        [Authorize(Roles = RolesModel.Admin)]
        public async Task<ActionResult> Add([FromBody] CityCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<CityCreateDto>(request));
            return Ok(res);
        }
    }
}
