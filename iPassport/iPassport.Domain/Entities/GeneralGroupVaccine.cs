using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class GeneralGroupVaccine : Entity
    {
        public Guid VaccineId { get; private set; }
        public Guid? PeriodTypeId { get; private set; }
        public int RequiredDoses { get; private set; }
        public int MaxTimeNextDose { get; private set; }
        public int MinTimeNextDose { get; private set; }

        public virtual VaccinePeriodType PeriodType { get; set; }
        public virtual Vaccine Vaccine { get; set; }

        public static GeneralGroupVaccine Create(GeneralGroupVaccineDto dto, Guid vaccineId)
         => new GeneralGroupVaccine()
         {
             Id = Guid.NewGuid(),
             VaccineId = vaccineId,
             MaxTimeNextDose = dto.TimeNextDoseMax,
             MinTimeNextDose = dto.TimeNextDoseMin,
             PeriodTypeId = dto.PeriodTypeId,
             RequiredDoses = dto.RequiredDoses
         };
    }
}
