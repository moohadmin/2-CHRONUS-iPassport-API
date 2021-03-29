using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Imported File Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImportedFileController : ControllerBase
    {
        /// <summary>
        /// Auto Mapper property
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Imported File service property
        /// </summary>
        private readonly IImportedFileService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Imported File service instance</param>
        /// <param name="mapper">Auto Mapper instence</param>
        public ImportedFileController(IMapper mapper, IImportedFileService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// This API is responsible for Get Imported Files By Date.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>List of Imported Files.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetByPeriod([FromQuery]GetImportedFileRequest request)
        {
            var res = await _service.FindByPeriod(_mapper.Map<GetImportedFileFilter>(request));
            return Ok(res);
        }

        /// <summary>
        /// This API is responsible for Get Import file details list.
        /// </summary>
        /// <param name="id">Import file Id</param>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Import file details list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet("{id}/Details")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var res = await _service.GetImportedFileDetails(id);
            return Ok(res);
        }
    }
}
