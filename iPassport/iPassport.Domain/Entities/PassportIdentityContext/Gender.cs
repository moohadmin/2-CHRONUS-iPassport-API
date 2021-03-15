namespace iPassport.Domain.Entities
{
    public class Gender : Entity
    {
        public Gender() { }
        public Gender(string name)
        {
            Id = System.Guid.NewGuid();
            Name = name;          
        }

        public string Name { get; private set; }
     
    }
}
