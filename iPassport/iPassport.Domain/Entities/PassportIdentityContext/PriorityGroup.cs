namespace iPassport.Domain.Entities
{
    public class PriorityGroup : Entity
    {
        public PriorityGroup() { }
        public PriorityGroup(string name)
        {
            Id = System.Guid.NewGuid();
            Name = name;          
        }

        public string Name { get; private set; }
     
    }
}
