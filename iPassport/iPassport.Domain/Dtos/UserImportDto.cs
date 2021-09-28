using iPassport.Domain.Utils;
using System;

namespace iPassport.Domain.Dtos
{
    public class UserImportDto
    {
        private readonly int CNPJ_SIZE = 14;
        private readonly int CPF_SIZE = 11;

        private string _cpf;
        private string _cnpj;
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }

        
        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                _cpf = !string.IsNullOrEmpty(value) ? value.Trim().PadLeft(CPF_SIZE, '0') : value;
            }
        }
        public string Cns { get; set; }
        public string Cnpj  {
            get
            {
                return _cnpj;
            }
            set
            {
                _cnpj = !string.IsNullOrEmpty(value) ? value.Trim().PadLeft(CNPJ_SIZE, '0') : value;
            }
        }
        public string Occupation { get; set; }
        public string Bond { get; set; }
        public string PriorityGroup { get; set; }
        public string BloodType { get; set; }
        public string HumanRace { get; set; }
        public int? CountryCode { get; set; }
        public ulong? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string WasCovidInfected { get; set; }
        public string WasTestPerformed { get; set; }
        public DateTime? TestDate { get; set; }
        public string Result { get; set; }
        public DateTime? ResultDate { get; set; }

        public string VaccineNameUniqueDose { get; set; }
        public string VaccineManufacturerNameUniqueDose { get; set; }
        public string BatchUniqueDose { get; set; }
        public DateTime? VaccinationDateUniqueDose { get; set; }
        public string EmployeeNameVaccinationUniqueDose { get; set; }
        public string EmployeeCpfVaccinationUniqueDose { get; set; }
        public string EmployeeCorenVaccinationUniqueDose { get; set; }
        public string HealthUnityCnpjUniqueDose { get; set; }
        public string HealthUnityIneUniqueDose { get; set; }
        public int? HealthUnityCodeUniqueDose { get; set; }
        public bool HasVaccineUniqueDoseData
        {
            get => !string.IsNullOrEmpty(VaccineNameUniqueDose)
                || !string.IsNullOrEmpty(VaccineManufacturerNameUniqueDose)
                || !string.IsNullOrEmpty(BatchUniqueDose)
                || VaccinationDateUniqueDose.HasValue
                || !string.IsNullOrEmpty(EmployeeNameVaccinationUniqueDose)
                || !string.IsNullOrEmpty(EmployeeCpfVaccinationUniqueDose)
                || !string.IsNullOrEmpty(EmployeeCorenVaccinationUniqueDose)
                || !string.IsNullOrEmpty(HealthUnityCnpjUniqueDose)
                || !string.IsNullOrEmpty(HealthUnityIneUniqueDose)
                || HealthUnityCodeUniqueDose.HasValue;
        }

        public string VaccineNameFirstDose { get; set; }
        public string VaccineManufacturerNameFirstDose { get; set; }
        public string BatchFirstDose { get; set; }
        public DateTime? VaccinationDateFirstDose { get; set; }
        public string EmployeeNameVaccinationFirstDose { get; set; }
        public string EmployeeCpfVaccinationFirstDose { get; set; }
        public string EmployeeCorenVaccinationFirstDose { get; set; }
        public string HealthUnityCnpjFirstDose { get; set; }
        public string HealthUnityIneFirstDose { get; set; }
        public int? HealthUnityCodeFirstDose { get; set; }
        public bool HasVaccineFirstDoseData
        {
            get => !string.IsNullOrEmpty(VaccineNameFirstDose)
                || !string.IsNullOrEmpty(VaccineManufacturerNameFirstDose)
                || !string.IsNullOrEmpty(BatchFirstDose)
                || VaccinationDateFirstDose.HasValue
                || !string.IsNullOrEmpty(EmployeeNameVaccinationFirstDose)
                || !string.IsNullOrEmpty(EmployeeCpfVaccinationFirstDose)
                || !string.IsNullOrEmpty(EmployeeCorenVaccinationFirstDose)
                || !string.IsNullOrEmpty(HealthUnityCnpjFirstDose)
                || !string.IsNullOrEmpty(HealthUnityIneFirstDose)
                || HealthUnityCodeFirstDose.HasValue;
        }

        public string VaccineNameSecondDose { get; set; }
        public string VaccineManufacturerNameSecondDose { get; set; }
        public string BatchSecondDose { get; set; }
        public DateTime? VaccinationDateSecondDose { get; set; }
        public string EmployeeNameVaccinationSecondDose { get; set; }
        public string EmployeeCpfVaccinationSecondDose { get; set; }
        public string EmployeeCorenVaccinationSecondDose { get; set; }
        public string HealthUnityCnpjSecondDose { get; set; }
        public string HealthUnityIneSecondDose { get; set; }
        public int? HealthUnityCodeSecondDose { get; set; }
        public bool HasVaccineSecondDoseData
        {
            get => !string.IsNullOrEmpty(VaccineNameSecondDose)
                || !string.IsNullOrEmpty(VaccineManufacturerNameSecondDose)
                || !string.IsNullOrEmpty(BatchSecondDose)
                || VaccinationDateSecondDose.HasValue
                || !string.IsNullOrEmpty(EmployeeNameVaccinationSecondDose)
                || !string.IsNullOrEmpty(EmployeeCpfVaccinationSecondDose)
                || !string.IsNullOrEmpty(EmployeeCorenVaccinationSecondDose)
                || !string.IsNullOrEmpty(HealthUnityCnpjSecondDose)
                || !string.IsNullOrEmpty(HealthUnityIneSecondDose)
                || HealthUnityCodeSecondDose.HasValue;
        }

        public string VaccineNameThirdDose { get; set; }
        public string VaccineManufacturerNameThirdDose { get; set; }
        public string BatchThirdDose { get; set; }
        public DateTime? VaccinationDateThirdDose { get; set; }
        public string EmployeeNameVaccinationThirdDose { get; set; }
        public string EmployeeCpfVaccinationThirdDose { get; set; }
        public string EmployeeCorenVaccinationThirdDose { get; set; }
        public string HealthUnityCnpjThirdDose { get; set; }
        public string HealthUnityIneThirdDose { get; set; }
        public int? HealthUnityCodeThirdDose { get; set; }
        public bool HasVaccineThirdDoseData
        {
            get => !string.IsNullOrEmpty(VaccineNameThirdDose)
                || !string.IsNullOrEmpty(VaccineManufacturerNameThirdDose)
                || !string.IsNullOrEmpty(BatchThirdDose)
                || VaccinationDateThirdDose.HasValue
                || !string.IsNullOrEmpty(EmployeeNameVaccinationThirdDose)
                || !string.IsNullOrEmpty(EmployeeCpfVaccinationThirdDose)
                || !string.IsNullOrEmpty(EmployeeCorenVaccinationThirdDose)
                || !string.IsNullOrEmpty(HealthUnityCnpjThirdDose)
                || !string.IsNullOrEmpty(HealthUnityIneThirdDose)
                || HealthUnityCodeThirdDose.HasValue;
        }

        public Guid? GenderId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? PriorityGroupId { get; set; }
        public Guid? BloodTypeId { get; set; }
        public Guid? HumanRaceId { get; set; }
        public Guid CityId { get; set; }
        public Guid? VaccineIdUniqueDose { get; set; }
        public Guid? HealthUnityIdUniqueDose { get; set; }
        public Guid? VaccineIdFirstDose { get; set; }
        public Guid? HealthUnityIdFirstDose { get; set; }
        public Guid? VaccineIdSecondDose { get; set; }
        public Guid? HealthUnityIdSecondDose { get; set; }
        public Guid? VaccineIdThirdDose { get; set; }
        public Guid? HealthUnityIdThirdDose { get; set; }
        public bool? WasCovidInfectedBool
        {
            get => WasCovidInfected.ToUpper() == Constants.CONST_NENHUM_VALUE ? null : WasCovidInfected.ToUpper() == Constants.CONST_SIM_VALUE;
        }
        public bool? ResultBool
        {
            get => Result.ToUpper() == Constants.CONST_NENHUM_VALUE ? null : Result.ToUpper() == Constants.CONST_POSITIVO_VALUE;
        }
        public Guid UserId { get; set; }

        public string Complement { get; set; }
        public string RG { get; set; }
        public string InternationalDocument { get; set; }
        public string PassportDoc { get; set; }
    }
}
