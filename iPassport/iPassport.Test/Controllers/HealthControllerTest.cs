using iPassport.Api.Controllers;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class HealthControllerTest
    {
        Mock<IHealthService> _mockService;
        public HealthController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IHealthService>();

            _controller = new HealthController(_mockService.Object) { };
        }

        [TestMethod]
        public void GetAll_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetAll()).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", null)));

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
