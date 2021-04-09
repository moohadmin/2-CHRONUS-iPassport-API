using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Filters
{
    public class GetHeadquarterCompanyFilter
    {
        public string Cnpj { get; set; }
        public ECompanySegmentType SegmentIdentifyer { get; set; }
        public ECompanyType CompanyTypeIdentifyer { get; set; }
        public Guid? LocalityId { get; set; }

        public bool IsPrivate() => CompanyTypeIdentifyer == ECompanyType.Private;
        public bool IsPublicState() => CompanyTypeIdentifyer == ECompanyType.Government && SegmentIdentifyer == ECompanySegmentType.State;
        public bool IsPublicMunicipal() => CompanyTypeIdentifyer == ECompanyType.Government && SegmentIdentifyer == ECompanySegmentType.Municipal;
    }
}
