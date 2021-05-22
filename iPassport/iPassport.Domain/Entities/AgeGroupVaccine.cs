using iPassport.Domain.Dtos;
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

        public static AgeGroupVaccine Create(AgeGroupVaccineDto dto, Guid vaccineId)
        => new()
        {
            Id = Guid.NewGuid(),
            VaccineId = vaccineId,
            FinalAgeGroup = dto.FinalAgeGroup,
            InitalAgeGroup = dto.InitalAgeGroup,
            MaxTimeNextDose = dto.MaxTimeNextDose,
            MinTimeNextDose = dto.MinTimeNextDose,
            PeriodTypeId = dto.PeriodTypeId,
            RequiredDoses = dto.RequiredDoses
        };
    }
}
