using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

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
        [Obsolete("This Property is Deprecated")]
        public string UnitName { get; private set; }
        public string Batch { get; private set; }
        public string EmployeeName { get; private set; }
        public string EmployeeCpf { get; private set; }
        public string EmployeeCoren { get; private set; }
        /// <summary>
        /// Depreciated field moved to HealthUnit entity
        /// </summary>
        [Obsolete("This Property is Deprecated")]
        public Guid? CityId { get; private set; }
        /// <summary>
        /// Depreciated field moved to HealthUnit entity
        /// </summary>
        [Obsolete("This Property is Deprecated")]
        public int? UnityType { get; private set; }
        public Guid? HealthUnitId { get; private set; }
        public DateTime? ExclusionDate { get; private set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual HealthUnit HealthUnit { get; set; }


        public UserVaccine Create(UserVaccineCreateDto dto)
            => new UserVaccine(dto.VaccinationDate, dto.Dose, dto.VaccineId, dto.UserId, dto.Batch, dto.EmployeeName, dto.EmployeeCpf, dto.EmployeeCoren, dto.HealthUnitId);
        public UserVaccine Create(UserVaccineEditDto dto)
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

        public void Change(UserVaccineEditDto dto)
        {
            Batch = dto.Batch;
            Dose = dto.Dose;
            EmployeeCoren = dto.EmployeeCoren;
            EmployeeCpf = dto.EmployeeCpf;
            EmployeeName = dto.EmployeeName;
            VaccinationDate = dto.VaccinationDate;

            VaccineId = dto.VaccineId;
            HealthUnitId = dto.HealthUnitId;
        }

        public void Delete() => ExclusionDate = DateTime.UtcNow;

        public static IEnumerable<UserVaccine> CreateListUserVaccine(UserImportDto dto)
        {
            List<UserVaccine> userVacines = new();
            if (dto.HasVaccineUniqueDoseData)
                userVacines.Add(new UserVaccine(dto.VaccinationDateUniqueDose.Value, 1, dto.VaccineIdUniqueDose.Value, dto.UserId, dto.BatchUniqueDose, dto.EmployeeNameVaccinationUniqueDose, dto.EmployeeCpfVaccinationUniqueDose, dto.EmployeeCorenVaccinationUniqueDose, dto.HealthUnityIdUniqueDose.Value));

            if (dto.HasVaccineFirstDoseData)
                userVacines.Add(new UserVaccine(dto.VaccinationDateFirstDose.Value, 1, dto.VaccineIdFirstDose.Value, dto.UserId, dto.BatchFirstDose, dto.EmployeeNameVaccinationFirstDose, dto.EmployeeCpfVaccinationFirstDose, dto.EmployeeCorenVaccinationFirstDose, dto.HealthUnityIdFirstDose.Value));

            if (dto.HasVaccineSecondDoseData)
                userVacines.Add(new UserVaccine(dto.VaccinationDateSecondDose.Value, 2, dto.VaccineIdSecondDose.Value, dto.UserId, dto.BatchSecondDose, dto.EmployeeNameVaccinationSecondDose, dto.EmployeeCpfVaccinationSecondDose, dto.EmployeeCorenVaccinationSecondDose, dto.HealthUnityIdSecondDose.Value));

            if (dto.HasVaccineThirdDoseData)
                userVacines.Add(new UserVaccine(dto.VaccinationDateThirdDose.Value, 3, dto.VaccineIdThirdDose.Value, dto.UserId, dto.BatchThirdDose, dto.EmployeeNameVaccinationThirdDose, dto.EmployeeCpfVaccinationThirdDose, dto.EmployeeCorenVaccinationThirdDose, dto.HealthUnityIdThirdDose.Value));

            return userVacines;
        }

        public bool CanEditVaccineFields(AccessControlDTO accessControl, UserVaccineEditDto itemChangedDto)
        {

            if (accessControl.Profile == EProfileKey.government.ToString() || accessControl.Profile == EProfileKey.healthUnit.ToString())
                return Batch == itemChangedDto.Batch
                    && Dose == itemChangedDto.Dose
                    && EmployeeCoren == itemChangedDto.EmployeeCoren
                    && EmployeeCpf == itemChangedDto.EmployeeCpf
                    && EmployeeName == itemChangedDto.EmployeeName
                    && VaccinationDate == itemChangedDto.VaccinationDate
                    && VaccineId == itemChangedDto.VaccineId
                    && HealthUnitId == itemChangedDto.HealthUnitId;

            return accessControl.Profile == EProfileKey.admin.ToString();
        }
    }
}
