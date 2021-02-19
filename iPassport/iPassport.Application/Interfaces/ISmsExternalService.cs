using iPassport.Domain.Dtos.SmsIntegration.GetSmsResult;
using iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsRequest;
using iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsResponse;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface ISmsExternalService 
    {
        /// <summary>
        /// Finds data from the sent message
        /// </summary>
        /// <param name="idMessage">Message id to search</param>
        /// <returns></returns>
        Task<SmsReportResponse> FindPin(string idMessage);
        /// <summary>
        /// Envia mensagem SMS
        /// </summary>
        /// <param name="smsAdvancedTextualRequest">Dto with the data for sending the SMS message</param>
        /// <returns></returns>
        Task<SmsSendReponse> SendPin(SmsAdvancedTextualRequest smsAdvancedTextualRequest);        
    }
}
