using AutoMapper;
using iPassport.Api.Models;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
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
    /// Vaccine Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccineController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Vaccine Service Property
        /// </summary>
        private readonly IVaccineService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper instance</param>
        /// <param name="service">Vaccine service instance</param>
        public VaccineController(IMapper mapper, IVaccineService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for get the Vaccinated count.
        /// </summary>
        /// <param name="request">Get Vaccinated count Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Vaccinated count.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("VaccinatedCount")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.HealthUnit, RolesModel.Business)]
        public async Task<ActionResult> GetVaccinatedCount([FromQuery] GetVaccinatedCountRequest request)
        {
            var res = await _service.GetVaccinatedCount(_mapper.Map<GetVaccinatedCountFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get paged list of Vacinne by name and Manufacutrer.
        /// </summary>
        /// <param name="request">Get Vaccines Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Vaccines list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Manufacturer")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetByManufacturerId([FromQuery] GetPagedVaccinesByManufacuterRequest request)
        {
            var res = await _service.GetByManufacturerId(_mapper.Map<GetByIdAndNamePartsPagedFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API add Vaccine
        /// </summary>
        /// <param name="request">Vaccine Create Request</param>
        /// <returns>Created Vaccine id</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> Add([FromBody] VaccineCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<VaccineDto>(request));
            return Ok(res);
        }
    }
}
