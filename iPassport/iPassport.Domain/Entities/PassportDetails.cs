namespace iPassport.Domain.Entities
{
    public class PassportDetails : Entity
    {
        public PassportDetails() { }

        public System.Guid PassportId { get; private set; }
        
        /// <summary>
        /// Expiration Data
        /// </summary>
        public System.DateTime ExpirationDate { get; private set; }
        
    }
}
