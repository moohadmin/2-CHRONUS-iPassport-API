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

        public Vaccine(string name, Guid manufacturerId, int expirationTime, int imunizationTime) : base()
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
        public Guid? DeactivationUserId { get; private set; }
        public DateTime? DeactivationDate { get; private set; }

        public virtual VaccineManufacturer Manufacturer { get; set; }
        public virtual IList<Disease> Diseases { get; set; }
        public virtual IList<UserVaccine> UserVaccines { get; set; }
        public virtual VaccineDosageType DosageType { get; set; }
        public virtual IList<AgeGroupVaccine> AgeGroupVaccines { get; set; }
        public virtual GeneralGroupVaccine GeneralGroupVaccine { get; set; }

        public static Vaccine Create(VaccineDto dto)
        {
            var vaccineId = Guid.NewGuid();
            var generalGroup = dto.GeneralGroupVaccine != null ? GeneralGroupVaccine.Create(dto.GeneralGroupVaccine, vaccineId) : null;
            var ageGroup = dto.AgeGroupVaccines != null && dto.AgeGroupVaccines.Any() ? dto.AgeGroupVaccines.Select(a => AgeGroupVaccine.Create(a, vaccineId)).ToList() : null;
            var vaccine = new Vaccine()
            {
                Id = vaccineId,
                Name = dto.Name,
                DosageTypeId = dto.DosageTypeId,
                ExpirationTimeInMonths = dto.ExpirationTimeInMonths,
                ImmunizationTimeInDays = dto.ImmunizationTimeInDays,
                ManufacturerId = dto.Manufacturer,
                GeneralGroupVaccine = generalGroup,
                AgeGroupVaccines = ageGroup,
                DeactivationUserId = dto.DeactivationUserId
            };

            return vaccine;
        }

        public bool IsActive() => DeactivationUserId == null && DeactivationDate == null;

        public void Deactivate(Guid deactivationUserId)
        {
            DeactivationUserId = deactivationUserId;
            DeactivationDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        public void Activate()
        {
            DeactivationUserId = null;
            DeactivationDate = null;
            UpdateDate = DateTime.UtcNow;
        }

        public bool UniqueDose()
        {
            return (GeneralGroupVaccine != null && GeneralGroupVaccine.RequiredDoses == 1)
                || (AgeGroupVaccines.Any() && AgeGroupVaccines.All(x => x.RequiredDoses == 1));
        }

        public int GetRequiredDoses(DateTime userBirthday)
        {
            if (AgeGroupVaccines.Any())
            {
                var today = DateTime.Now.Date;
                var age = today.Year - userBirthday.Year;

                var ageGroup = AgeGroupVaccines.FirstOrDefault(v => v.InitalAgeGroup <= age && v.FinalAgeGroup >= age);

                if (ageGroup != null) return ageGroup.RequiredDoses;
            }
            else if(GeneralGroupVaccine != null)
            {
                return GeneralGroupVaccine.RequiredDoses;
            }

            return 0;
        }

        public EVaccinePeriodType GetVaccinePeriodType()
        {
            if (AgeGroupVaccines.Any())
                return (EVaccinePeriodType)AgeGroupVaccines.FirstOrDefault().PeriodType.Identifyer;
         
            else
                return (EVaccinePeriodType)GeneralGroupVaccine.PeriodType.Identifyer;
        }

        public double GetMinTimeNextDose()
        {
            if (AgeGroupVaccines.Any())
                return AgeGroupVaccines.FirstOrDefault().MinTimeNextDose;

            else
                return GeneralGroupVaccine.MinTimeNextDose;
        }

        public double GetMaxTimeNextDose()
        {
            if (AgeGroupVaccines.Any())
                return AgeGroupVaccines.FirstOrDefault().MaxTimeNextDose;

            else
                return GeneralGroupVaccine.MaxTimeNextDose;
        }

        public void AddDiseases(IList<Disease> diseases)
        {
            if (Diseases == null)
                Diseases = new List<Disease>();

            foreach (var d in diseases)
                Diseases.Add(d);
        }
    }
}
