namespace iPassport.Domain.Entities
{
    public class Plan : Entity
    {
        public Plan() { }

        public Plan(string type, string description, decimal price)
        {
            Type = type;
            Description = description;
            Price = price;
        }

        public string Type { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}
