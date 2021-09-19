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
    /// Disease Controller 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiseaseController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Disease Service Property
        /// </summary>
        private readonly IDiseaseService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="service">Disease Service Instance</param>
        public DiseaseController(IMapper mapper, IDiseaseService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for get paged list of Diseases by name.
        /// </summary>
        /// <param name="request">Get Disease Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Diseases list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByNameInitals([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.GetByNameInitals(_mapper.Map<GetByNamePartsPagedFilter>(request));

            return Ok(res);
        }
    }
}
