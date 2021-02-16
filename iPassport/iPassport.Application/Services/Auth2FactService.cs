using iPassport.Application.Interfaces;


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
        /// Teste Consulta de Envio de SMS
        /// </summary>
        public void AuthClientRecieve()
        {

            var result = _smsExternalServices.GetSmsResult(null);
            
        }

        /// <summary>
        /// Teste Envio de SMS
        /// </summary>
        public void AuthClientSend()
        {

            var result = _smsExternalServices.SendSmsMessage(null);
        }
    }
}
