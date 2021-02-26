using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class Auth2FactMobile : Entity
    {
        public Auth2FactMobile() { }

        public Auth2FactMobile(Guid userId, string phone, string pin, bool isValid, string messageId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Phone = phone;
            Pin = pin;
            IsValid = isValid;
            MessageId = messageId;
        }

        public Guid UserId { get; private set; }
        public string Phone { get; private set; }
        public string Pin { get; private set; }
        public bool IsValid { get; private set; }
        public string MessageId { get; private set; }

        public Auth2FactMobile Create(Auth2FactMobileDto dto) => new Auth2FactMobile(dto.UserId, dto.Phone, dto.Pin, dto.IsValid, dto.MessageId);

        public bool CanUseToValidate() => (IsValid && CreateDate.AddHours(12) > DateTime.Now);

        public bool PreventsResendingPIN() => (IsValid && CreateDate.AddMinutes(2) > DateTime.Now);

        public void SetInvalid() => IsValid = false;
    }
}
