namespace iPassport.Domain.Entities
{
    public class Passport : Entity
    {
        public Passport() { }
        /// <summary>
        /// User Id
        /// </summary>
        public System.Guid UserId { get; private set; }
        /// <summary>
        /// Passport Id
        /// </summary>
        public int PassId { get; private set; }
        
        
    }
}
