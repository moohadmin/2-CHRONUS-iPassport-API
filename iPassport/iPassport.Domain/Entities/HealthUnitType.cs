using System;

namespace iPassport.Domain.Entities
{
    public class HealthUnitType : Entity
    {
        public HealthUnitType() { }

        public HealthUnitType(string name, int identifyer) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            Identifyer = identifyer;
        }

        public string Name { get; private set; }
        public int Identifyer { get; private set; }
    }
}
