using System;

using iPassport.Domain.Entities;
using iPassport.Domain.Enums;

namespace iPassport.Domain.Filters
{
    public class GetPagedVaccinesFilter : PageFilter
    {
        public string Initials { get; set; }
        public Guid? ManufacuterId { get; set; }
        public Guid? DiseaseId { get; set; }

        public EVaccineDosageType? DosageTypeId { get; set; }
        public VaccineDosageType VaccineDosageType { get; set; }
    }
}
