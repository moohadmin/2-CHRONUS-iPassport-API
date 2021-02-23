using iPassport.Api.Controllers;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
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
        PassportController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IPassportService>();
            _controller = new PassportController(_mockService.Object);
            
        }

        
        [TestMethod]
        public void Get_MustReturnOk()
        {
            var seed = PassportSeed.Get();

            // Arrange
            _controller.ControllerContext = ControllerContextFactory.Create();
            _mockService.Setup(r => r.Get("7d4bb42-a0cb-4a72-abaa-ded84823a166")).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", seed)));

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
