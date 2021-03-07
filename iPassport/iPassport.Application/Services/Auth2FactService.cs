using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Services.Constants;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System;
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
            var pinValid = await _auth2FactRepository.FindByUserAndPin(userId, pin);

            if (pinValid == null)
                throw new BusinessException("PIN de autenticação inválido. Favor conferir PIN enviado.");
            if(!pinValid.CanUseToValidate())
                throw new BusinessException("PIN expirado!");

            pinValid.SetInvalid();
            _auth2FactRepository.Update(pinValid);

            return pinValid;
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

            userPinList?.Where(x => x.IsValid)?.ToList()?.ForEach(x => 
                { x.SetInvalid();
                  _auth2FactRepository.Update(x);
                });

            var pin = PinGenerate();
            var text = $"{pin} - Is your iPassport verification PIN.";

            var resultPin = await _smsExternalServices.SendPin(text, phone);

            return await SaveAuth2FactMobile(userId, phone, pin, resultPin.Messages.FirstOrDefault()?.MessageId);
        }

        public async Task<Auth2FactMobile> SaveAuth2FactMobile(Guid userId, string phone, string pin, string MessageId)
        {
            var AmbienteSimulado = EnvConstants.NOTIFICATIONS_MOCK;

            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
                pin = "1111";

            var twoFactDto = new Auth2FactMobileDto
            {
                UserId = userId,
                Phone = phone,
                Pin = pin,
                IsValid = true,
                MessageId = MessageId
            };

            var twoFact = new Auth2FactMobile();
            var twoFactCreated = twoFact.Create(twoFactDto);
            await _auth2FactRepository.InsertAsync(twoFactCreated);

            return twoFactCreated;
        }
    }
}
