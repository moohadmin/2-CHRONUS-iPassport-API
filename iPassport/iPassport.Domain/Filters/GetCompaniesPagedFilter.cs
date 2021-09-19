using System;

namespace iPassport.Domain.Filters
{
    public class GetCompaniesPagedFilter : PageFilter
    {
        public string Initials { get; set; }
        public string Cnpj { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? SegmentId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CountryId { get; set; }
    }
}
