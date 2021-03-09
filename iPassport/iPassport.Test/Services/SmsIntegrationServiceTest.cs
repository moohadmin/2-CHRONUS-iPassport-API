using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.PinIntegration.FindPin;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using iPassport.Infra.ExternalServices;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class SmsIntegrationServiceTest
    {

        ISmsExternalService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new SmsIntegrationService();
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
