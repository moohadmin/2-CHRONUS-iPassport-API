using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Filters
{
    public class GetCitzenPagedFilter : PageFilter
    {
        public string Initials { get; set; }

        public EDocumentType? DocumentType { get; set; }

        public string Document { get; set; }

        public string Telephone { get; set; }

        public Guid? CityId { get; set; }

        public Guid? StateId { get; set; }

        public Guid? CountryId { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
