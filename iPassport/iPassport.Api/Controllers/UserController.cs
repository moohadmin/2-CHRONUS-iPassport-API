using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
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
        private readonly IUserVaccineService _vaccineService;

        public UserController(IMapper mapper, IUserService userService, IUserVaccineService vaccineService)
        {
            _mapper = mapper;
            _service = userService;
            _vaccineService = vaccineService;
        }

        /// <summary>
        /// This API Create User
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Citizen")]
        public async Task<ActionResult> AddCitizen([FromBody] CitizenCreateRequest request)
        {
            var res = await _service.AddCitizen(_mapper.Map<CitizenCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This Add User Image
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Operation OK<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
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
        /// <response code="204">Server returns no data.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
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
        /// <response code="204">Server returns no data.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
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
        /// <response code="204">Server returns no data.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
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
        /// <response code="204">Server returns no data.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="404">NotFound Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Current")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var res = await _service.GetCurrentUser();
            return Ok(res);
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
        [HttpGet("Vaccine")]
        public async Task<ActionResult> GetPagedUserVaccines([FromQuery] GetPagedUserVaccinesRequest request)
        {
            var res = await _vaccineService.GetUserVaccines(_mapper.Map<GetByIdPagedFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is get the get logged Citzen count
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("LoggedCitzenCount")]
        public async Task<ActionResult> GetLoggedCitzenCount()
        {
            var res = await _service.GetLoggedCitzenCount();
            return Ok(res);
        }

        /// <summary>
        /// This API Get the User registered count
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
        [HttpGet("RegisteredUsersCount")]
        public async Task<ActionResult> GetRegisteredUsersCount([FromQuery] GetRegisteredUsersCountRequest request)
        {
            var res = await _service.GetRegisteredUserCount(_mapper.Map<GetRegisteredUserCountFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is get the get logged Agent count
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.<</response>
        /// <response code="400">Bussiness Exception<</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now<</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("LoggedAgentCount")]
        public async Task<ActionResult> GetLoggedAgentCount()
        {
            var res = await _service.GetLoggedAgentCount();
            return Ok(res);
        }

        /// <summary>
        /// This API Create User Agent
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Agent")]
        public async Task<ActionResult> AddAgent([FromBody] UserAgentCreateRequest request)
        {
            var res = await _service.AddAgent(_mapper.Map<UserAgentCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// Get countries by part of name
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>Country</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="404">NotFound Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("CitizenByName")]
        public async Task<ActionResult> GetCitizenByNameParts([FromQuery] GetByNamePartsPagedRequest request)
        {
            var res = await _service.FindCitizensByNameParts(_mapper.Map<GetByNamePartsPagedFilter>(request));

            return Ok(res);
        }

    }
}
