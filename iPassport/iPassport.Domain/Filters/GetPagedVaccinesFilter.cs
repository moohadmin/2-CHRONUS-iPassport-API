using System;

namespace iPassport.Domain.Filters
{
    public class GetPagedVaccinesFilter : PageFilter
    {
        public string Initials { get; set; }
        public Guid? ManufacuterId { get; set; }
        public Guid? DiseaseId { get; set; }
        public Guid? DosageTypeId { get; set; }
    }
}
