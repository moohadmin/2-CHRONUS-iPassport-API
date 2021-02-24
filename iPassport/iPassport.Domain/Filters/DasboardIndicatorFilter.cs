using System;

namespace iPassport.Domain.Filters
{
    public class DasboardIndicatorFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid Vaccine { get; set; }
        public string VaccineLaboratory { get; set; }
    }
}
