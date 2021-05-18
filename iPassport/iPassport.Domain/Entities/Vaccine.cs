using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class Vaccine : Entity
    {
        public Vaccine() { }

        public Vaccine(string name, Guid manufacturerId, int requiredDoses, int expirationTime, int imunizationTime,int maxTimeNextDose, int minTimeNextDose) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            ManufacturerId = manufacturerId;
            ExpirationTimeInMonths = expirationTime;
            ImmunizationTimeInDays = imunizationTime;
        }
        
        public string Name { get; private set; }
        public int ExpirationTimeInMonths { get; private set; }
        public int ImmunizationTimeInDays { get; private set; }
        public Guid ManufacturerId { get; private set; }
        public Guid DosageTypeId { get; private set; }

        public virtual VaccineManufacturer Manufacturer { get; set; }
        public virtual IEnumerable<Disease> Diseases { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }
        public virtual VaccineDosageType DosageType { get; set; }
        public virtual IEnumerable<AgeGroupVaccineDosageType> AgeGroupVaccineDosage { get; set; }
        public virtual GeneralVaccineDosageType GeneralVaccineDosage { get; set; }

        public Vaccine Create(VaccineCreateDto dto) => new Vaccine(dto.Name, dto.ManufacturerId, dto.RequiredDoses, dto.ExpirationTime, dto.ImunizationTime, dto.MaxTimeNextDose, dto.MinTimeNextDose);

        public bool UniqueDose()
        {
            return (GeneralVaccineDosage != null && GeneralVaccineDosage.RequiredDoses == 1)
                || (AgeGroupVaccineDosage.Any() && AgeGroupVaccineDosage.All(x => x.RequiredDoses == 1));
        }

        public int GetRequiredDoses(DateTime userBirthday)
        {
            if (AgeGroupVaccineDosage.Any())
            {
                var today = DateTime.Now.Date;
                var age = today.Year - userBirthday.Year;

                var ageGroup = AgeGroupVaccineDosage.FirstOrDefault(v => v.InitalAgeGroup >= age && v.FinalAgeGroup <= age);

                if (ageGroup != null) return ageGroup.RequiredDoses;
            }
            else if(GeneralVaccineDosage != null)
            {
                return GeneralVaccineDosage.RequiredDoses;
            }

            return 0;
        }

        public EVaccinePeriodType GetVaccinePeriodType()
        {
            if (AgeGroupVaccineDosage.Any())
                return (EVaccinePeriodType)AgeGroupVaccineDosage.FirstOrDefault().PeriodType.Identifyer;
         
            else
                return (EVaccinePeriodType)GeneralVaccineDosage.PeriodType.Identifyer;
        }

        public double GetMinTimeNextDose()
        {
            if (AgeGroupVaccineDosage.Any())
                return AgeGroupVaccineDosage.FirstOrDefault().MinTimeNextDose;

            else
                return GeneralVaccineDosage.MinTimeNextDose;
        }

        public double GetMaxTimeNextDose()
        {
            if (AgeGroupVaccineDosage.Any())
                return AgeGroupVaccineDosage.FirstOrDefault().MaxTimeNextDose;

            else
                return GeneralVaccineDosage.MaxTimeNextDose;
        }
    }
}
