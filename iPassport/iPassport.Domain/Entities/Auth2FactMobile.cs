using System;
using iPassport.Domain.Dtos;

namespace iPassport.Domain.Entities
{
    public class Auth2FactMobile : Entity
    {
        public Auth2FactMobile() { }

        public Auth2FactMobile(Guid userId, string phone, string pin, bool isUsed, string messageId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Phone = phone;
            Pin = pin;
            IsUsed = isUsed;
            MessageId = messageId;
        }

        public Guid UserId { get; private set; }
        public string Phone { get; private set; }
        public string Pin { get; private set; }
        public bool IsUsed { get; private set; }
        public string MessageId { get; private set; }

        public Auth2FactMobile Create(Auth2FactMobileDto dto) => new Auth2FactMobile(dto.UserId, dto.Phone, dto.Pin, dto.IsUsed, dto.MessageId);

        public bool CanUseToValidate() => !(IsUsed || CreateDate.AddHours(12) < DateTime.Now);

        public bool PreventsResendingPIN() => (!IsUsed && CreateDate.AddMinutes(2) > DateTime.Now && CreateDate.AddHours(12) > DateTime.Now);
    }
}
