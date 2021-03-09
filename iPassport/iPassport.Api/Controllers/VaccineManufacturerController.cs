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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccineManufacturerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineManufacturerService _service;

        public VaccineManufacturerController(IMapper mapper, IVaccineManufacturerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API Get the User vaccines
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="404">NotFound Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
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
