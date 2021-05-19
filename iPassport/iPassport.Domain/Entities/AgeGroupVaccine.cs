using System;

namespace iPassport.Domain.Entities
{
    public class AgeGroupVaccine : Entity
    {
        public Guid VaccineId { get; private set; }
        public Guid PeriodTypeId { get; private set; }
        public int RequiredDoses { get; private set; }
        public int MaxTimeNextDose { get; private set; }
        public int MinTimeNextDose { get; private set; }
        public int InitalAgeGroup { get; private set; }
        public int FinalAgeGroup { get; private set; }

        public virtual VaccinePeriodType PeriodType { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
