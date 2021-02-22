namespace iPassport.Domain.Entities
{
    public class PassportDetails : Entity
    {
        public PassportDetails() { }

        /// <summary>
        /// Passport Id
        /// </summary>
        public System.Guid PassportId { get; private set; }        
        
        /// <summary>
        /// Expiration Data
        /// </summary>
        public System.DateTime ExpirationDate { get; private set; }
        
        /// <summary>
        /// Passport
        /// </summary>
        public virtual Passport Passport { get; set; }
    }
}
