using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class UserVaccine : Entity
    {
        public UserVaccine() { }

        public UserVaccine(DateTime vaccinationDate, int dose, Guid vaccineId, Guid userId, Guid cityId, string unitName, string batch, string employeeName, string employeeCpf, string employeeCoren, int unityType)
        {
            Id = Guid.NewGuid();
            VaccinationDate = vaccinationDate;
            Dose = dose;
            VaccineId = vaccineId;
            UserId = userId;
            CityId = cityId;
            UnitName = unitName;
            Batch = batch;
            EmployeeName = employeeName;
            EmployeeCpf = employeeCpf;
            EmployeeCoren = employeeCoren;
            UnityType = unityType;
        }

        public DateTime VaccinationDate { get; private set; }
        public int Dose { get; private set; }
        public Guid VaccineId { get; private set; }
        public Guid UserId { get; private set; }
        public string UnitName { get; private set; }
        public string Batch { get; private set; }
        public string EmployeeName { get; private set; }
        public string EmployeeCpf { get; private set; }
        public string EmployeeCoren { get; private set; }
        public Guid CityId { get; private set; }
        public int UnityType { get; private set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual Vaccine Vaccine { get; set; }

        public UserVaccine Create(UserVaccineCreateDto dto)
            => new UserVaccine(dto.VaccinationDate, dto.Dose, dto.VaccineId, dto.UserId, dto.City, dto.UnitName, dto.Batch, dto.EmployeeName, dto.EmployeeCpf, dto.EmployeeCoren, (int)dto.UnityType);
        
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
