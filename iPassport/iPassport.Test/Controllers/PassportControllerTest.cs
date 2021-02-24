using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class PassportControllerTest
    {
        Mock<IPassportService> _mockService;
        IMapper _mapper;
        IHttpContextAccessor _accessor;
        PassportController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IPassportService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new PassportController(_mockService.Object, _mapper);
            _accessor = HttpContextAccessorFactory.Create();
        }

        
        [TestMethod]
        public void Get_MustReturnOk()
        {
            var seed = PassportSeed.Get();

            // Arrange
            _controller.ControllerContext = ControllerContextFactory.Create();
            _mockService.Setup(r => r.Get()).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", seed)));

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
