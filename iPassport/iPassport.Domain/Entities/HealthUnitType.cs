using System;

namespace iPassport.Domain.Entities
{
    public class HealthUnitType : Entity
    {
        public HealthUnitType() { }

        public HealthUnitType(string name) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; private set; }
    }
}
