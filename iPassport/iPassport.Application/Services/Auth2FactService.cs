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
            var text = $"{pin} - Is your iPassport verification PIN.";

            var resultPin = await _smsExternalServices.SendPin(text, phone);

            return await SaveAuth2FactMobile(userId, phone, pin, resultPin.Messages.FirstOrDefault()?.MessageId);
        }

        public async Task<Auth2FactMobile> ValidPin(Guid userId, string pin)
        {
            var pinvalid = await _auth2FactRepository.FindByUserAndPin(userId, pin);

            if (pinvalid == null)
                throw new BusinessException("Código de autenticação inválido. Favor conferir código enviado.");
            if(!pinvalid.CanUseToValidate())
                throw new BusinessException("PIN expirado!");

            return pinvalid;
        }

        private string PinGenerate()
        {
            Random number = new Random();
            var pin = number.Next().ToString().Substring(0, 4);
            return pin;
        }

        public async Task<Auth2FactMobile> ResendPin(Guid userId, string phone)
        {
            var userPinList = await _auth2FactRepository.FindByUser(userId);
            if (userPinList != null && userPinList.Any(x => x.PreventsResendingPIN()))
                throw new BusinessException("Favor aguardar antes de solicitar novo pin");

            var pin = PinGenerate();
            var text = $"{pin} - Is your iPassport verification PIN.";

            var resultPin = await _smsExternalServices.SendPin(text, phone);

            return await SaveAuth2FactMobile(userId, phone, pin, resultPin.Messages.FirstOrDefault()?.MessageId);
        }

        public async Task<Auth2FactMobile> SaveAuth2FactMobile(Guid userId, string phone, string pin, string MessageId)
        {
            var AmbienteSimulado = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
                pin = "1111";

            var twoFactDto = new Auth2FactMobileDto
            {
                UserId = userId,
                Phone = phone,
                Pin = pin,
                IsUsed = false,
                MessageId = MessageId
            };

            var twoFact = new Auth2FactMobile();
            var twoFactCreated = twoFact.Create(twoFactDto);
            await _auth2FactRepository.InsertAsync(twoFactCreated);

            return twoFactCreated;
        }
    }
}
