using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class UserVaccine : Entity
    {
        public UserVaccine() { }

        public UserVaccine(DateTime vaccinationDate, int dose, Guid vaccineId, Guid userId)
        {
            Id = Guid.NewGuid();
            VaccinationDate = vaccinationDate;
            Dose = dose;
            VaccineId = vaccineId;
            UserId = userId;
        }

        public DateTime VaccinationDate { get; private set; }
        public int Dose { get; private set; }
        public Guid VaccineId { get; private set; }
        public Guid UserId { get; private set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual Vaccine Vaccine { get; set; }

        public UserVaccine Create(UserVaccineCreateDto dto) => new UserVaccine(dto.VaccinationDate, dto.Dose, dto.VaccineId, dto.UserId);
        public DateTime GetExpirationDate(Vaccine vaccine) => VaccinationDate.AddMonths(vaccine.ExpirationTimeInMonths);
        public bool IsImmunized()
        {
            var today = DateTime.UtcNow.Date;
            if (Vaccine == null)
                return false;

            if (VaccinationDate.Date.AddDays(Vaccine.ImmunizationTimeInDays) <= today // Time for the vaccine to start taking effect
                && VaccinationDate.Date.AddMonths(Vaccine.ExpirationTimeInMonths) >= today // Vaccine shelf life
                && Dose == Vaccine.RequiredDoses) // Amount of required dosage
                return true;

            return false;
        }

        public bool IsFirstDose() => Dose == 1;
    }
}
