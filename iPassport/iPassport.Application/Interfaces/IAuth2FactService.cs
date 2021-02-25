using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using iPassport.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IAuth2FactService
    {
        /// <summary>
        /// Query Sent PIN by idMessage
        /// </summary>
        /// <param name="idMessage">idMessage sent to client</param>
        void FindPinSent(string idMessage);

        Task<Auth2FactMobile> SendPin(Guid userId, string phone);
        Task<Auth2FactMobile> ValidPin(Guid userId, string pin);
    }
}
