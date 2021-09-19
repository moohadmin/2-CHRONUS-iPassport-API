using iPassport.Domain.Dtos;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

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
            // 4 - CNS
            MapUsing((entity, values) =>
            {
                entity.Cns = values.Tokens[4] == string.Empty ? null : values.Tokens[4];
                return true;
            });
            MapProperty(5, x => x.RG);
            MapProperty(6, x => x.PassportDoc);
            MapProperty(7, x => x.InternationalDocument);
            MapProperty(8, x => x.Cnpj);
            MapProperty(9, x => x.Occupation);
            MapProperty(10, x => x.Bond);
            MapProperty(11, x => x.PriorityGroup);
            MapProperty(12, x => x.BloodType);
            MapProperty(13, x => x.HumanRace);
            MapProperty(14, x => x.CountryCode);
            MapProperty(15, x => x.PhoneNumber);
            // 16 - Email
            MapUsing((entity, values) =>
            {
                entity.Email = values.Tokens[16] == string.Empty ? null : values.Tokens[16];
                return true;
            });
            MapProperty(17, x => x.Cep);
            MapProperty(18, x => x.Address);
            MapProperty(19, x => x.Number);
            MapProperty(20, x => x.Complement);
            MapProperty(21, x => x.District);
            MapProperty(22, x => x.City);
            MapProperty(23, x => x.State);
            MapProperty(24, x => x.Country);
            MapProperty(25, x => x.WasCovidInfected);
            MapProperty(26, x => x.WasTestPerformed);
            MapProperty(27, x => x.TestDate);
            MapProperty(28, x => x.Result);
            MapProperty(29, x => x.ResultDate);

            MapProperty(30, x => x.VaccineNameUniqueDose);
            MapProperty(31, x => x.VaccineManufacturerNameUniqueDose);
            MapProperty(32, x => x.BatchUniqueDose);
            MapProperty(33, x => x.VaccinationDateUniqueDose);
            MapProperty(34, x => x.EmployeeNameVaccinationUniqueDose);
            MapProperty(35, x => x.EmployeeCpfVaccinationUniqueDose);
            MapProperty(36, x => x.EmployeeCorenVaccinationUniqueDose);
            MapProperty(37, x => x.HealthUnityCnpjUniqueDose);
            MapProperty(38, x => x.HealthUnityIneUniqueDose);
            MapProperty(39, x => x.HealthUnityCodeUniqueDose);

            MapProperty(40, x => x.VaccineNameFirstDose);
            MapProperty(41, x => x.VaccineManufacturerNameFirstDose);
            MapProperty(42, x => x.BatchFirstDose);
            MapProperty(43, x => x.VaccinationDateFirstDose);
            MapProperty(44, x => x.EmployeeNameVaccinationFirstDose);
            MapProperty(45, x => x.EmployeeCpfVaccinationFirstDose);
            MapProperty(46, x => x.EmployeeCorenVaccinationFirstDose);
            MapProperty(47, x => x.HealthUnityCnpjFirstDose);
            MapProperty(48, x => x.HealthUnityIneFirstDose);
            MapProperty(49, x => x.HealthUnityCodeFirstDose);

            MapProperty(50, x => x.VaccineNameSecondDose);
            MapProperty(51, x => x.VaccineManufacturerNameSecondDose);
            MapProperty(52, x => x.BatchSecondDose);
            MapProperty(53, x => x.VaccinationDateSecondDose);
            MapProperty(54, x => x.EmployeeNameVaccinationSecondDose);
            MapProperty(55, x => x.EmployeeCpfVaccinationSecondDose);
            MapProperty(56, x => x.EmployeeCorenVaccinationSecondDose);
            MapProperty(57, x => x.HealthUnityCnpjSecondDose);
            MapProperty(58, x => x.HealthUnityIneSecondDose);
            MapProperty(59, x => x.HealthUnityCodeSecondDose);

            MapProperty(60, x => x.VaccineNameThirdDose);
            MapProperty(61, x => x.VaccineManufacturerNameThirdDose);
            MapProperty(62, x => x.BatchThirdDose);
            MapProperty(63, x => x.VaccinationDateThirdDose);
            MapProperty(64, x => x.EmployeeNameVaccinationThirdDose);
            MapProperty(65, x => x.EmployeeCpfVaccinationThirdDose);
            MapProperty(66, x => x.EmployeeCorenVaccinationThirdDose);
            MapProperty(67, x => x.HealthUnityCnpjThirdDose);
            MapProperty(68, x => x.HealthUnityIneThirdDose);
            MapProperty(69, x => x.HealthUnityCodeThirdDose);
        }
    }
}
