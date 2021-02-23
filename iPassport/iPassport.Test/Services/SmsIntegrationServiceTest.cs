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
            SendPinRequestDto _sendPinRequestDto = new SendPinRequestDto()
            {
                Messages = new MessageDto()
                {
                    Text = "Cod Validation 1234",
                    Destinations = new List<DestinationDto>(){
                        new DestinationDto()
                        {
                            To = "12345678"
                        }
                    }
                 }
            };

            // Act
            var result = _service.SendPin(_sendPinRequestDto);

            // Assert            
            Assert.IsInstanceOfType(result, typeof(Task<SendPinResponseDto>));
        }
    }
}
