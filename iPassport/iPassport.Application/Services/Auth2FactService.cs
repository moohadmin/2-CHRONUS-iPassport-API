using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class Auth2FactService : IAuth2FactService
    {
        private readonly ISmsExternalService _smsExternalServices;
        private readonly IAuth2FactMobileRepository _auth2FactRepository;
        private readonly IStringLocalizer<Resource> _localizer;

        public Auth2FactService(ISmsExternalService smsExternalServices, IAuth2FactMobileRepository auth2FactMobileRepository, IStringLocalizer<Resource> localizer)
        {
            _smsExternalServices = smsExternalServices;
            _auth2FactRepository = auth2FactMobileRepository;
            _localizer = localizer;
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
            var userPinList = await _auth2FactRepository.FindActiveByUser(userId);
            if(userPinList != null)
            {
                foreach (var item in userPinList)
                {
                    item.SetInvalid();
                    await _auth2FactRepository.Update(item);
                }
            }
            

            var pin = PinGenerate();
            var text = string.Format(_localizer["PinGenerated"], pin);

            var resultPin = await _smsExternalServices.SendPin(text, phone);

            return await SaveAuth2FactMobile(userId, phone, pin, resultPin.Messages.FirstOrDefault()?.MessageId);
        }

        public async Task<Auth2FactMobile> ValidPin(Guid userId, string pin)
        {
            var pinValid = await _auth2FactRepository.FindByUserAndPin(userId, pin);

            if (pinValid == null)
                throw new BusinessException(_localizer["PinInvalid"]);
            if(!pinValid.CanUseToValidate())
                throw new BusinessException(_localizer["PinExpired"]);

            pinValid.SetInvalid();
            await _auth2FactRepository.Update(pinValid);

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
            var userPinList = await _auth2FactRepository.FindActiveByUser(userId);
            if (userPinList != null && userPinList.Any(x => x.PreventsResendingPIN()))
                throw new BusinessException(_localizer["PinResendTime"]);

            if (userPinList != null)
            {
                foreach (var item in userPinList)
                {
                    item.SetInvalid();
                    await _auth2FactRepository.Update(item);
                }
            }

            var pin = PinGenerate();
            var text = string.Format(_localizer["PinGenerated"], pin);

            var resultPin = await _smsExternalServices.SendPin(text, phone);

            return await SaveAuth2FactMobile(userId, phone, pin, resultPin.Messages.FirstOrDefault()?.MessageId);
        }

        public async Task<Auth2FactMobile> SaveAuth2FactMobile(Guid userId, string phone, string pin, string MessageId)
        {
            var simulateAmbient = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(simulateAmbient) && Convert.ToBoolean(simulateAmbient))
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
