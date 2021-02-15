namespace iPassport.Domain.Entities
{
    public class Health : Entity
    {
        public Health()
        {
            Id = System.Guid.NewGuid();
            Status = false;
        }

        public bool Status { get; private set; }

        public void Heahty() => Status = true;
    }
}
