using System;

namespace iPassport.Domain.Entities
{
    public class CompanySegment : Entity
    {
        public CompanySegment() { }

        public CompanySegment(string name, int identifyer, Guid companyTypeId) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            Identifyer = identifyer;
            CompanyTypeId = companyTypeId;
        }

        public string Name { get; private set; }
        public int Identifyer { get; private set; }
        public Guid CompanyTypeId { get; private set; }

        public CompanyType CompanyType { get; set; }
    }
}
