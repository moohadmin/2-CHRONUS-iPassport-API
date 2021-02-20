using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;

namespace iPassport.Application.Services
{
    public class Auth2FactService : IAuth2FactService
    {
        private readonly ISmsExternalService _smsExternalServices;
        public Auth2FactService(ISmsExternalService smsExternalServices)
        {
            _smsExternalServices = smsExternalServices;
        }

        /// <summary>
        /// Query Sent PIN by idMessage
        /// </summary>
        /// <param name="idMessage">idMessage sent to client</param>
        public void FindPinSent(string idMessage)
        {           
            var result = _smsExternalServices.FindPinSent(idMessage);
        }

        /// <summary>
        /// Send Pin to client
        /// </summary>
        public void SendPin()
        {            
            var result = _smsExternalServices.SendPin(new SendPinRequestDto());            
        }
    }
}
