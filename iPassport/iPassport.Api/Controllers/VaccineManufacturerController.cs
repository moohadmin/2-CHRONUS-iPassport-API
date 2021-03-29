using AutoMapper;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Vaccine Manufacturer Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccineManufacturerController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Vaccine Manufacturer Service Property
        /// </summary>
        private readonly IVaccineManufacturerService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper instance</param>
        /// <param name="service">Vaccine Manufacturer service instance</param>
        public VaccineManufacturerController(IMapper mapper, IVaccineManufacturerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for Get paged list of Vaccine Manufacuter by name.
        /// </summary>
        /// <param name="request">Get Vaccines Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Vaccines Maufacurer list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetByNameInitals([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.GetByNameInitals(_mapper.Map<GetByNamePartsPagedFilter>(request));

            return Ok(res);
        }
    }
}
