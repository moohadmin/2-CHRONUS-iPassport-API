using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

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

        public Auth2FactMobile SendPin(Guid userId, string phone)
        {
            var pin = PinGenerate();

            var dests = new List<DestinationDto>();
            dests.Add(new DestinationDto { To = phone });

            var messageDto = new MessageDto
            {
                From = "Moohtech",
                Destinations = dests,
                Text = pin
            };

            var sendPinRequestDto = new SendPinRequestDto
            {
                Messages = messageDto
            };
            
            var resultPin = _smsExternalServices.SendPin(sendPinRequestDto);

            var message = resultPin.Result.Messages.FirstOrDefault();

            var twoFact = new Auth2FactMobile();
            twoFact.Create(userId, phone, pin, false, message.MessageId);

            _auth2FactRepository.InsertAsync(twoFact);

            return twoFact;
        }

        private string PinGenerate()
        {
            Random number = new Random();
            var pin = number.Next().ToString().Substring(0, 4);
            return pin;
        }
    }
}
