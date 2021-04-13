using iPassport.Domain.Enums;
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

        public bool IsPrivateType() => CompanyType.IsPrivate();
        public bool IsGovernmentType() => CompanyType.IsGovernment();
        public bool IsMunicipal() => Identifyer == (int)ECompanySegmentType.Municipal;
        public bool IsState() => Identifyer == (int)ECompanySegmentType.State;
        public bool IsFederal() => Identifyer == (int)ECompanySegmentType.Federal;
    }
}
