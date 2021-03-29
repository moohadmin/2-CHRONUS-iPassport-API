namespace iPassport.Domain.Entities
{
    public class BloodType : Entity
    {
        public BloodType() { }
        public BloodType(string name)
        {
            Id = System.Guid.NewGuid();
            Name = name;          
        }

        public string Name { get; private set; }
     
    }
}
