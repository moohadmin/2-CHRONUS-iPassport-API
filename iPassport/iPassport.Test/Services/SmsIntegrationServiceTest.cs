using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.PinIntegration.FindPin;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using iPassport.Infra.ExternalServices;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class SmsIntegrationServiceTest
    {

        ISmsExternalService _service;
        IConfiguration _config;        

        [TestInitialize]
        public void Setup()
        {
            _config = Mock.Of<IConfiguration>();
            _service = new SmsIntegrationService(_config);
            EnvVariablesFactory.Create();
        }

        
        [TestMethod]
        public void FindPinSent_MustReturnOk()
        {
            // Arrange
            

            // Act
            var result = _service.FindPinSent("12345678");

            // Assert            
            Assert.IsInstanceOfType(result, typeof(Task<PinReportResponseDto>));
        }

        
        [TestMethod]
        public void SendPin_MustReturnOk()
        {
            // Arrange
            var text = "Cod Validation 1234";
            var to = "12345678";


            // Act
            var result = _service.SendPin(text,to);

            // Assert            
            Assert.IsInstanceOfType(result, typeof(Task<SendPinResponseDto>));
        }
    }
}
