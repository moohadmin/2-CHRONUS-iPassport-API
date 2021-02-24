using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class Passport : Entity
    {
        public Passport() { }

        public Passport(UserDetails userDetails )
        {
            Id = Guid.NewGuid();
            ListPassportDetails = CreateFirstPassportDetails();
            UserDetails = userDetails;
            
        }

        /// <summary>
        /// Passport Id
        /// </summary>
        public int PassId { get; private set; }
        
        /// <summary>
        /// UserDetails Id
        /// </summary>
        public Guid UserDetailsId { get; private set; }

        /// <summary>
        /// UserDetails
        /// </summary>
        public UserDetails UserDetails { get; private set; }
        /// <summary>
        /// List of Passport Details
        /// </summary>
        public virtual IEnumerable<PassportDetails> ListPassportDetails { get; set; }

        /// <summary>
        /// Create Passport Details
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PassportDetails> CreateFirstPassportDetails()
        {
            return new List<PassportDetails>()
            {
                new PassportDetails().Create(GetExpirationDate(null), this)
            };
        }

        /// <summary>
        /// Create Passport
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns>new Passport</returns>
        public Passport Create(UserDetails userDetails) => new Passport(userDetails);
        /// <summary>
        /// Get FriendlyPass ID
        /// </summary>
        /// <returns></returns>
        public string GetPassId() => PassId.ToString("i-000000");
        /// <summary>
        /// Get last PassportDetails
        /// </summary>
        /// <returns></returns>
        public PassportDetails GetLastPassportDetails() => ListPassportDetails.OrderBy(x => x.CreateDate).LastOrDefault();
        /// <summary>
        /// Get Expiration Date
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public DateTime GetExpirationDate(int? days) =>  days.HasValue ? DateTime.Today.AddDays(days.Value) : DateTime.Today.AddMonths(6);

        public bool IsAllDetailsExpired() => ListPassportDetails.All(x => x.IsExpired());

        public PassportDetails NewPassportDetails(int? ExpirationTime)
        {
            var details = new PassportDetails();
            details = details.Create(GetExpirationDate(ExpirationTime), this);
            return details;
        }

    }
}
