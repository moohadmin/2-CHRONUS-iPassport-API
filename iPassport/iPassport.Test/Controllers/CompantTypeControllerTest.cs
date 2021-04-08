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
    public class CompantTypeControllerTest
    {
        Mock<ICompanyTypeService> _mockService;
        public CompanyTypeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICompanyTypeService>();

            _controller = new CompanyTypeController(_mockService.Object) { };
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
