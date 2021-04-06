using AutoMapper;
using iPassport.Api.Models;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Priority Group Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriorityGroupController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Priority Group Service Property
        /// </summary>
        private readonly IPriorityGroupService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper Instance</param>
        /// <param name="priorityGroupService">Priority Group Service Instance</param>
        public PriorityGroupController(IMapper mapper, IPriorityGroupService priorityGroupService)
        {
            _mapper = mapper;
            _service = priorityGroupService;
        }

        /// <summary>
        /// This API is responsible for Get paged list of Priority Group by name.
        /// </summary>
        /// <param name="request">Get Priority Group Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Priority Group list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> GetByNameParts([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.FindByNameParts(_mapper.Map<GetByNamePartsPagedFilter>(request));

            return Ok(res);
        }
    }
}
