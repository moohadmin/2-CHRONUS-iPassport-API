using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlanService _service;

        public PlanController(IMapper mapper, IPlanService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API Create Plan
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlanCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<PlanCreateDto>(request));
            return Ok(res);
        }


    }
}
