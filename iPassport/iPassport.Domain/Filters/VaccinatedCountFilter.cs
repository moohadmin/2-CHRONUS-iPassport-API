using System;

namespace iPassport.Domain.Filters
{
    public class VaccinatedCountFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? DiseaseId { get; set; }
        public Guid? ManufacturerId { get; set; }
        public int? DosageCount { get; set; }
    }
}
