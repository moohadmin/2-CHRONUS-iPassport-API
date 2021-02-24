using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class PassportDetails : Entity
    {
        public PassportDetails() { }

        public PassportDetails(DateTime expirationDate, Guid passportId) : base()
        {
            Id = Guid.NewGuid();
            ExpirationDate = expirationDate;            
            PassportId = passportId;
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

        public PassportDetails Create(DateTime expirationDate, Passport passport) => new PassportDetails(expirationDate, passport.Id);
        public PassportDetails Create(DateTime expirationDate, Guid PassportId) => new PassportDetails(expirationDate, PassportId);

        public bool IsExpired() => ExpirationDate < DateTime.Today;
    }
}
