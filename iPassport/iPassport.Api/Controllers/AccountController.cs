using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  Account Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Account Service Property
        /// </summary>
        private readonly IAccountService _service;
        
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="accountService">Account Service Instance</param>
        public AccountController(IAccountService accountService) => _service = accountService;

        /// <summary>
        /// This API is responsible for Agent login.
        /// </summary>
        /// <param name="request">Agent login request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByAgent")]
        public async Task<ActionResult> BasicLogin([FromBody] BasicLoginRequest request)
        {
            var res = await _service.BasicLogin(request.Username, request.Password);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for E-mail login.
        /// </summary>
        /// <param name="request">Email login request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByEmail")]
        public async Task<ActionResult> LoginByEmail([FromBody] EmailLoginRequest request)
        {
            var res = await _service.EmailLogin(request.Email, request.Password);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Mobile login.
        /// </summary>
        /// <param name="request">Mobile login request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByCitizen")]
        public async Task<ActionResult> MobileLogin([FromBody] LoginMobileRequest request)
        {
            var res = await _service.MobileLogin(request.Pin, request.UserId, request.AcceptTerms);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible request pin verification.
        /// </summary>
        /// <param name="request">Pin request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("Pin")]
        public async Task<ActionResult> SendPin([FromBody] PinRequest request)
        {
            var res = await _service.SendPin(request.Phone, request.Doctype, request.Document);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for reset user password.
        /// </summary>
        /// <param name="request">Mobile Login Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [Authorize]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("PasswordReset")]
        public async Task<ActionResult> PasswordReset([FromBody] ResetPasswordRequest request)
        {
            var res = await _service.ResetPassword(request.Password, request.PasswordConfirm);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for resend pin verification request login.
        /// </summary>
        /// <param name="request">Resend Pin Request</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("ResendPin")]
        public async Task<ActionResult> ResendPin([FromBody] ResendPinRequest request)
        {
            var res = await _service.ResendPin(request.Phone, request.UserId);
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for logout the current user token.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Operation Result</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            var res = await _service.Logout();
            return Ok(res);
        }
    }
}