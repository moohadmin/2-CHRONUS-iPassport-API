using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Dtos
{
    public class AgeGroupVaccineDto
    {
        public Guid? Id { get; set; }
        public EVaccinePeriodType? PeriodType { get; set; }
        public Guid PeriodTypeId { get; set; }
        public int RequiredDoses { get; set; }
        public int TimeNextDoseMax { get; set; }
        public int TimeNextDoseMin { get; set; }
        public int AgeGroupInitial { get; set; }
        public int AgeGroupFinal { get; set; }
    }
}
