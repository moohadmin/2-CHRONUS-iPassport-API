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
    /// Gender Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenderController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Gender Service Property
        /// </summary>
        private readonly IGenderService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="genderService">gender Service Instance</param>
        public GenderController(IMapper mapper, IGenderService genderService)
        {
            _mapper = mapper;
            _service = genderService;
        }

        /// <summary>
        /// This API is responsible for Get paged list of Gender by name.
        /// </summary>
        /// <param name="request">Get Gender Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Genders list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByNameParts([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetByNamePartsPagedFilter>(request));

            return Ok(res);
        }
    }
}
