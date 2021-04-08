using System;

namespace iPassport.Domain.Entities
{
    public class CompanyType : Entity
    {
        public CompanyType() { }

        public CompanyType(string name, int identifyer) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            Identifyer = identifyer;
        }

        public string Name { get; private set; }
        public int Identifyer { get; private set; }
    }
}
