using AutoMapper;
using iPassport.Api.Models;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Responses;
using iPassport.Api.Security;
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
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper Property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// User Service Property
        /// </summary>
        private readonly IUserService _service;

        /// <summary>
        /// User Vaccine Service Property
        /// </summary>
        private readonly IUserVaccineService _vaccineService;

        /// <summary>
        /// User Disease Test Service Property
        /// </summary>
        private readonly IUserDiseaseTestService _userDiseaseTestService;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="mapper">Auto Mapper instance</param>
        /// <param name="userService">User service instance</param>
        /// <param name="vaccineService">User Vaccine service instance</param>
        /// <param name="userDiseaseTestService">User Disease Test service instance</param>
        public UserController(IMapper mapper, IUserService userService, IUserVaccineService vaccineService, IUserDiseaseTestService userDiseaseTestService)
        {
            _mapper = mapper;
            _service = userService;
            _vaccineService = vaccineService;
            _userDiseaseTestService = userDiseaseTestService;
        }

        /// <summary>
        /// This API is responsible for add Citizen.
        /// </summary>
        /// <param name="request">Citizen Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Citizen Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Citizen")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government)]
        public async Task<ActionResult> AddCitizen([FromBody] CitizenCreateRequest request)
        {
            var res = await _service.AddCitizen(_mapper.Map<CitizenCreateDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for add User Image.
        /// </summary>
        /// <param name="request">User Image Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Image Id</returns>
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
        /// This API is responsible for remove User Image.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpDelete("{userId}/Image")]
        public async Task<ActionResult> UserImageRemove(Guid userId)
        {
            var res = await _service.RemoveUserImage(userId);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for change User Plan.
        /// </summary>
        /// <param name="planId">Plan Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Associated Plan Id</returns>
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
        /// This API is responsible for add User Plan.
        /// </summary>
        /// <param name="planId">Plan Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Associated Plan Id</returns>
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
        /// This API is responsible for get User Plan.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Plan Object.</returns>
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
        /// This API is responsible for get current User.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Current User Object.</returns>
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
        /// This API is responsible for get User vaccines By your Passport Id.
        /// </summary>
        /// <param name="request">Get Paged User Vaccines Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User vaccines.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Vaccine")]
        public async Task<ActionResult> GetPagedUserVaccines([FromQuery] GetPagedUserVaccinesByPassportRequest request)
        {
            var res = await _vaccineService.GetUserVaccines(_mapper.Map<GetByIdPagedFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get current User vaccines.
        /// </summary>
        /// <param name="request">Get Paged User Vaccines Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User vaccines.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Vaccine/Current")]
        public async Task<ActionResult> GetPagedUserVaccines([FromQuery] PageFilterRequest request)
        {
            var res = await _vaccineService.GetCurrentUserVaccines(_mapper.Map<PageFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get the logged Citizen count.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Logged Citizen count.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("LoggedCitzenCount")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.Business, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetLoggedCitzenCount()
        {
            var res = await _service.GetLoggedCitzenCount();
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get the registered user count.
        /// </summary>
        /// <param name="request">Get registered User count Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Registered User count.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("RegisteredUsersCount")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.Business, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetRegisteredUsersCount([FromQuery] GetRegisteredUsersCountRequest request)
        {
            var res = await _service.GetRegisteredUserCount(_mapper.Map<GetRegisteredUserCountFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get the logged Agent count.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Logged Agent count.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("LoggedAgentCount")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.Business, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetLoggedAgentCount()
        {
            var res = await _service.GetLoggedAgentCount();
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get paged list of Agent by name.
        /// </summary>
        /// <param name="request">Get Agent Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Aegnt list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Agent")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Business)]
        public async Task<ActionResult> GetAgentByNameParts([FromQuery] GetAgentPagedRequest request)
        {
            var res = await _service.GetPagedAgent(_mapper.Map<GetAgentPagedFilter>(request));

            return Ok(res);
        }
        /// <summary>
        /// This API is responsible for Add Agent.
        /// </summary>
        /// <param name="request">Agent Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Agent Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("Agent")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> AddAgent([FromBody] UserAgentCreateRequest request)
        {
            var res = await _service.AddAgent(_mapper.Map<UserAgentDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for edit Agent.
        /// </summary>
        /// <param name="request">Agent edit Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Agent Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPut("Agent")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> EditAgent([FromBody] UserAgentEditRequest request)
        {
            var res = await _service.EditAgent(_mapper.Map<UserAgentDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get paged list of Citzen by name.
        /// </summary>
        /// <param name="request">Get Citzen Paged Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Paged Citzen list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("Citizen")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.Business, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetCitizenByNameParts([FromQuery] GetCitzenPagedRequest request)
        {
            var res = await _service.GetPaggedCizten(_mapper.Map<GetCitzenPagedFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get User Disease tests By your Passport Id.
        /// </summary>
        /// <param name="request">Get Paged User Disease tests Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Disease tests results.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Test")]
        public async Task<ActionResult> GetPagedUserTests([FromQuery] GetPagedUserVaccinesByPassportRequest request)
        {
            var res = await _userDiseaseTestService.GetUserDiseaseTest(_mapper.Map<GetByIdPagedFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get current User Disease tests.
        /// </summary>
        /// <param name="request">Get Paged User Disease tests Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Disease tests results.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Test/Current")]
        public async Task<ActionResult> GetPagedUserTests([FromQuery] PageFilterRequest request)
        {
            var res = await _userDiseaseTestService.GetCurrentUserDiseaseTest(_mapper.Map<PageFilter>(request));

            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get Citizen By Id.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Citizen Object.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Citizen/{id}")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.Business, RolesModel.HealthUnit)]
        public async Task<ActionResult> GetCitizenById(Guid id)
        {
            var res = await _service.GetCitizenById(id);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for edit Citizen.
        /// </summary>
        /// <param name="request">Citizen edit Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Citizen Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPut("Citizen")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government, RolesModel.HealthUnit)]
        public async Task<ActionResult> EditCitizen([FromBody] CitizenEditRequest request)
        {
            var res = await _service.EditCitizen(_mapper.Map<CitizenEditDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for import citizens by file.
        /// </summary>
        /// <param name="request">CSV file with user data.</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>User Image Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Import")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> ImportUsers([FromForm] UserImportRequest request)
        {
            await _service.ImportUsers(request.File);
            return Ok();
        }

        /// <summary>
        /// This API is responsible for add Admin.
        /// </summary>
        /// <param name="request">Admin Create Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Admin Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPost("Admin")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government)]
        public async Task<ActionResult> AddAdmin([FromBody] AdminCreateRequest request)
        {
            var res = await _service.AddAdmin(_mapper.Map<AdminDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get Admin By Id.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Admin Object.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Admin/{id}")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> GetAdminById(Guid id)
        {
            var res = await _service.GetAdminById(id);
            return Ok(res);
        }

        /// <summary>
        /// This API is get the get pagged admin users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Admin")]
        [AuthorizeRole(RolesModel.Admin, RolesModel.Government)]
        public async Task<ActionResult> GetPagedAdmins([FromQuery] GetAdminUserPagedRequest request)
        {
            var res = await _service.GetPagedAdmins(_mapper.Map<GetAdminUserPagedFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for edit Admin.
        /// </summary>
        /// <param name="request">Admin Edit Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Admin Id</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpPut("Admin")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> EditAdmin([FromBody] AdminEditRequest request)
        {
            var res = await _service.EditAdmin(_mapper.Map<AdminDto>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for get Agent By Id.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Agent Object.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [Authorize]
        [HttpGet("Agent/{id}")]
        [AuthorizeRole(RolesModel.Admin)]
        public async Task<ActionResult> GetAgentById(Guid id)
        {
            var res = await _service.GetAgentById(id);
            return Ok(res);
        }
    }
}
