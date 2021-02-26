using iPassport.Domain.Dtos.PinIntegration.FindPin;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
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
        Task<PinReportResponseDto> FindPinSent(string idMessage);
        
        Task<SendPinResponseDto> SendPin(string text, string phone);
    }
}
