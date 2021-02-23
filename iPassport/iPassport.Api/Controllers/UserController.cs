using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _service = userService;
        }

        /// <summary>
        /// This API Create User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserCreateRequest request)
        {
            var res = await _service.Add(_mapper.Map<UserCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This Add User Image
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Operation OK</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("UploadImage")]
        public async Task<ActionResult> UserImageUpload([FromForm] UserImageRequest request)
        {
            var res = await _service.AddUserImage(_mapper.Map<UserImageDto>(request));
            return Ok(res);

        }

        /// <summary>
        /// This API Associate Plan to User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPut("Plan")]
        public async Task<ActionResult> PutUserPlan(Guid planId)
        {
            var res = await _service.AssociatePlan(planId);
            return Ok(res);
        }

        /// <summary>
        /// This API Associate Plan to User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Plan")]
        public async Task<ActionResult> PostUserPlan(Guid planId)
        {
            var res = await _service.AssociatePlan(planId);
            return Ok(res);
        }

        /// <summary>
        /// This API Get User Plan
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Plan")]
        public async Task<ActionResult> GetUserPlan()
        {
            var res = await _service.GetUserPlan();
            return Ok(res);
        }

        /// <summary>
        /// This API Get the current User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Current")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var res = await _service.GetCurrentUser();
            return Ok(res);
        }
    }
}
