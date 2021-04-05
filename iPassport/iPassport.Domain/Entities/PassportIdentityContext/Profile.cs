namespace iPassport.Domain.Entities
{
    public class Profile : Entity
    {
        public Profile() { }
        public Profile(string name, string key)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Key = key;
        }
        public string Name { get; private set; }
        public string Key { get; private set; }

    }
}
