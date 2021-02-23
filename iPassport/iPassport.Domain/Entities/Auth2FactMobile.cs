using System;

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

        public Auth2FactMobile Create(Guid userId, string phone, string pin, bool isUsed, string messageId) =>
            new Auth2FactMobile(userId, phone, pin, isUsed, messageId);
    }
}
