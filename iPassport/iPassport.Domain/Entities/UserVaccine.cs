using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class UserVaccine : Entity
    {
        public UserVaccine() { }

        public UserVaccine(DateTime vaccinationDate, int dose, Guid vaccineId, Guid userId, string batch, string employeeName, string employeeCpf, string employeeCoren, Guid healthUnitId)
        {
            Id = Guid.NewGuid();
            VaccinationDate = vaccinationDate;
            Dose = dose;
            VaccineId = vaccineId;
            UserId = userId;
            Batch = batch;
            EmployeeName = employeeName;
            EmployeeCpf = employeeCpf;
            EmployeeCoren = employeeCoren;
            HealthUnitId = healthUnitId;
        }

        public DateTime VaccinationDate { get; private set; }
        public int Dose { get; private set; }
        public Guid VaccineId { get; private set; }
        public Guid UserId { get; private set; }
        /// <summary>
        /// Depreciated field moved to HealthUnit entity
        /// </summary>
        public string UnitName { get; private set; }
        public string Batch { get; private set; }
        public string EmployeeName { get; private set; }
        public string EmployeeCpf { get; private set; }
        public string EmployeeCoren { get; private set; }
        /// <summary>
        /// Depreciated field moved to HealthUnit entity
        /// </summary>
        public Guid? CityId { get; private set; }
        /// <summary>
        /// Depreciated field moved to HealthUnit entity
        /// </summary>
        public int? UnityType { get; private set; }
        public Guid? HealthUnitId { get; private set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual HealthUnit HealthUnit  { get; set; }


        public UserVaccine Create(UserVaccineCreateDto dto)
            => new UserVaccine(dto.VaccinationDate, dto.Dose, dto.VaccineId, dto.UserId, dto.Batch, dto.EmployeeName, dto.EmployeeCpf, dto.EmployeeCoren, dto.HealthUnitId);
        
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
