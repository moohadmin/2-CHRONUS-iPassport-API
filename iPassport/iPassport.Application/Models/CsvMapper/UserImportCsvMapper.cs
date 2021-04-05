using iPassport.Domain.Dtos;
using TinyCsvParser.Mapping;

namespace iPassport.Application.Models.CsvMapper
{
    public class UserImportCsvMapper : CsvMapping<UserImportDto>
    {
        public UserImportCsvMapper()
        : base()
        {
            MapProperty(0, x => x.FullName);
            MapProperty(1, x => x.Gender);
            MapProperty(2, x => x.Birthday);
            MapProperty(3, x => x.Cpf);
            MapProperty(4, x => x.Cns);
            MapProperty(5, x => x.Cnpj);
            MapProperty(6, x => x.Occupation);
            MapProperty(7, x => x.Bond);
            MapProperty(8, x => x.PriorityGroup);
            MapProperty(9, x => x.BloodType);
            MapProperty(10, x => x.HumanRace);
            MapProperty(11, x => x.CountryCode);
            MapProperty(12, x => x.PhoneNumber);
            MapProperty(13, x => x.Email);
            MapProperty(14, x => x.Cep);
            MapProperty(15, x => x.Address);
            MapProperty(16, x => x.Number);
            MapProperty(17, x => x.District);
            MapProperty(18, x => x.City);
            MapProperty(19, x => x.State);
            MapProperty(20, x => x.Country);
            MapProperty(21, x => x.WasCovidInfected);
            MapProperty(22, x => x.WasTestPerformed);
            MapProperty(23, x => x.TestDate);
            MapProperty(24, x => x.Result);
            MapProperty(25, x => x.ResultDate);

            MapProperty(26, x => x.VaccineNameUniqueDose);
            MapProperty(27, x => x.VaccineManufacturerNameUniqueDose);
            MapProperty(28, x => x.BatchUniqueDose);
            MapProperty(29, x => x.VaccinationDateUniqueDose);
            MapProperty(30, x => x.EmployeeNameVaccinationUniqueDose);
            MapProperty(31, x => x.EmployeeCpfVaccinationUniqueDose);
            MapProperty(32, x => x.EmployeeCorenVaccinationUniqueDose);
            MapProperty(33, x => x.HealthUnityCnpjUniqueDose);
            MapProperty(34, x => x.HealthUnityIneUniqueDose);
            MapProperty(35, x => x.HealthUnityCodeUniqueDose);

            MapProperty(36, x => x.VaccineNameFirstDose);
            MapProperty(37, x => x.VaccineManufacturerNameFirstDose);
            MapProperty(38, x => x.BatchFirstDose);
            MapProperty(39, x => x.VaccinationDateFirstDose);
            MapProperty(40, x => x.EmployeeNameVaccinationFirstDose);
            MapProperty(41, x => x.EmployeeCpfVaccinationFirstDose);
            MapProperty(42, x => x.EmployeeCorenVaccinationFirstDose);
            MapProperty(43, x => x.HealthUnityCnpjFirstDose);
            MapProperty(44, x => x.HealthUnityIneFirstDose);
            MapProperty(45, x => x.HealthUnityCodeFirstDose);

            MapProperty(46, x => x.VaccineNameSecondDose);
            MapProperty(47, x => x.VaccineManufacturerNameSecondDose);
            MapProperty(48, x => x.BatchSecondDose);
            MapProperty(49, x => x.VaccinationDateSecondDose);
            MapProperty(50, x => x.EmployeeNameVaccinationSecondDose);
            MapProperty(51, x => x.EmployeeCpfVaccinationSecondDose);
            MapProperty(52, x => x.EmployeeCorenVaccinationSecondDose);
            MapProperty(53, x => x.HealthUnityCnpjSecondDose);
            MapProperty(54, x => x.HealthUnityIneSecondDose);
            MapProperty(55, x => x.HealthUnityCodeSecondDose);

            MapProperty(56, x => x.VaccineNameThirdDose);
            MapProperty(57, x => x.VaccineManufacturerNameThirdDose);
            MapProperty(58, x => x.BatchThirdDose);
            MapProperty(59, x => x.VaccinationDateThirdDose);
            MapProperty(60, x => x.EmployeeNameVaccinationThirdDose);
            MapProperty(61, x => x.EmployeeCpfVaccinationThirdDose);
            MapProperty(62, x => x.EmployeeCorenVaccinationThirdDose);
            MapProperty(63, x => x.HealthUnityCnpjThirdDose);
            MapProperty(64, x => x.HealthUnityIneThirdDose);
            MapProperty(65, x => x.HealthUnityCodeThirdDose);
        }
    }
}
