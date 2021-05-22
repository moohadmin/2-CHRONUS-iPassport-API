using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Dtos
{
    public class GeneralGroupVaccineDto
    {
        public Guid? Id { get; set; }
        public EVaccinePeriodType PeriodType { get; set; }
        public Guid PeriodTypeId { get; set; }
        public int RequiredDoses { get; set; }
        public int MaxTimeNextDose { get; set; }
        public int MinTimeNextDose { get; set; }
    }
}
