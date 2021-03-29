namespace iPassport.Domain.Entities
{
    public class HumanRace : Entity
    {
        public HumanRace() { }
        public HumanRace(string name)
        {
            Id = System.Guid.NewGuid();
            Name = name;          
        }

        public string Name { get; private set; }
     
    }
}
