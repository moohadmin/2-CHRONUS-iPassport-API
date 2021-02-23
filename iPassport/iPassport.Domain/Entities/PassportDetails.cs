using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class PassportDetails : Entity
    {
        public PassportDetails() { }

        public PassportDetails(DateTime expirationDate, Passport passport) : base()
        {
            Id = Guid.NewGuid();
            ExpirationDate = expirationDate;
            Passport = passport;
        }

        /// <summary>
        /// Passport Id
        /// </summary>
        public Guid PassportId { get; private set; }        
        
        /// <summary>
        /// Expiration Data
        /// </summary>
        public DateTime ExpirationDate { get; private set; }
        
        /// <summary>
        /// Passport
        /// </summary>
        public virtual Passport Passport { get; set; }

        public virtual IEnumerable<PassportUse> PassportUses { get; set; }

        public PassportDetails Create(DateTime expirationDate, Passport passport) => new PassportDetails(expirationDate, passport);

        public bool IsExpired() => ExpirationDate < DateTime.Today;
    }
}
