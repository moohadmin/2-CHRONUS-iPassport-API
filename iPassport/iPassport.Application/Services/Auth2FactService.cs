using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class Auth2FactService : IAuth2FactService
    {
        private readonly ISmsExternalService _smsExternalServices;
        private readonly IAuth2FactMobileRepository _auth2FactRepository;

        public Auth2FactService(ISmsExternalService smsExternalServices, IAuth2FactMobileRepository auth2FactMobileRepository)
        {
            _smsExternalServices = smsExternalServices;
            _auth2FactRepository = auth2FactMobileRepository;
        }

        /// <summary>
        /// Query Sent PIN by idMessage
        /// </summary>
        /// <param name="idMessage">idMessage sent to client</param>
        public void FindPinSent(string idMessage)
        {           
            var result = _smsExternalServices.FindPinSent(idMessage);
        }

        public async Task<Auth2FactMobile> SendPin(Guid userId, string phone)
        {
            var pin = PinGenerate();

            var dests = new List<DestinationDto>();
            dests.Add(new DestinationDto { To = phone });

            var messageDto = new MessageDto
            {
                From = "Moohtech",
                Destinations = dests,
                Text = $"{pin} - Is your iPassport verification PIN."
            };

            var sendPinRequestDto = new SendPinRequestDto
            {
                Messages = messageDto
            };
            
            var resultPin = await _smsExternalServices.SendPin(sendPinRequestDto);

            var message = resultPin.Messages.FirstOrDefault();

            var AmbienteSimulado = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
                pin = "1111";
            
            var twoFactDto = new Auth2FactMobileDto 
            {
                UserId = userId,
                Phone = phone,
                Pin = pin,
                IsUsed = false,
                MessageId = message.MessageId
            };

            var twoFact = new Auth2FactMobile();
            var twoFactCreated = twoFact.Create(twoFactDto);
            await _auth2FactRepository.InsertAsync(twoFactCreated);

            return twoFactCreated;
        }

        public async Task<Auth2FactMobile> ValidPin(Guid userId, string pin)
        {
            var pinvalid = await _auth2FactRepository.FindByUserAndPin(userId, pin);

            if(pinvalid == null || !pinvalid.CanUseToValidate())
                throw new BusinessException("PIN inválido!");           

            return pinvalid;
        }

        private string PinGenerate()
        {
            Random number = new Random();
            var pin = number.Next().ToString().Substring(0, 4);
            return pin;
        }
    }
}
