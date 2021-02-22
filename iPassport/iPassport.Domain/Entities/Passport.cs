using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Passport : Entity
    {
        public Passport() { }
        
        /// <summary>
        /// Passport Id
        /// </summary>
        public int PassId { get; private set; }
        
        /// <summary>
        /// UserDetails Id
        /// </summary>
        public System.Guid UserDetailsId { get; private set; }

        /// <summary>
        /// UserDetails
        /// </summary>
        public UserDetails UserDetails { get; private set; }
        /// <summary>
        /// List of Passport Details
        /// </summary>
        public virtual IEnumerable<PassportDetails> PassportDetails { get; set; }

    }
}
