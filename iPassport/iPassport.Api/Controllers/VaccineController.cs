using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineService _service;

        public VaccineController(IMapper mapper, IVaccineService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is get the get vaccinated count
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("VaccinatedCount")]
        public async Task<ActionResult> GetVaccinatedCount([FromQuery] GetVaccinatedCountRequest request)
        {
            var res = await _service.GetVaccinatedCount(_mapper.Map<GetVaccinatedCountFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is get the get vaccines by your manufacuter
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Manufacturer")]
        public async Task<ActionResult> GetByManufacturerId([FromQuery] GetPagedVaccinesByManufacuterRequest request)
        {
            var res = await _service.GetByManufacturerId(_mapper.Map<GetByIdAndNamePartsPagedFilter>(request));
            return Ok(res);
        }
    }
}
