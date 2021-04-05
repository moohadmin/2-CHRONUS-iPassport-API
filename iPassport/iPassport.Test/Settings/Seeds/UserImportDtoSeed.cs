using iPassport.Domain.Dtos;
using System;

namespace iPassport.Test.Settings.Seeds
{
    public static class UserImportDtoSeed
    {
        private static UserImportDto UserImportDtoComplete()
            => new()
            {
                FullName = "Full Name",
                Gender = "Feminino",
                Birthday = DateTime.Now.AddYears(-20),
                Cpf = "88869127060",
                Cns = "123456789012345",
                Cnpj = "16894522000114",
                Occupation = "Occupation",
                Bond = "Bond",
                PriorityGroup = "PriorityGroup",
                BloodType = "A+",
                HumanRace = "Indígena",
                CountryCode = 55,
                PhoneNumber = 912345678,
                Email = "fullname@email.com",
                Cep = "01310000",
                Address = "Av. Paulista",
                Number = "2302",
                District = "Consolação",
                City = "São Paulo",
                State = "São Paulo",
                Country = "Brasil",
                WasCovidInfected = "Sim",
                WasTestPerformed = "Sim",
                TestDate = DateTime.Now.AddDays(-2),
                Result = "Negativo",
                ResultDate = DateTime.Now.AddDays(-1),
                VaccineNameUniqueDose = "Coronavac",
                VaccineManufacturerNameUniqueDose = "Sinovac/Butantan",
                BatchUniqueDose = "123",
                VaccinationDateUniqueDose = DateTime.Now,
                EmployeeNameVaccinationUniqueDose = "Employee Name Dose Unique",
                EmployeeCpfVaccinationUniqueDose = "76666310063",
                EmployeeCorenVaccinationUniqueDose = "COREN123",
                HealthUnityCnpjUniqueDose = "04931716000163",
                HealthUnityIneUniqueDose = "123456789",
                HealthUnityCodeUniqueDose = null,
                VaccineNameFirstDose = "Covishield (ChAdOx1 nCoV-19/ AZD1222)",
                VaccineManufacturerNameFirstDose = "Pfizer/BioNTech",
                BatchFirstDose = "456",
                VaccinationDateFirstDose = DateTime.Now,
                EmployeeNameVaccinationFirstDose = "Employee Name Dose First",
                EmployeeCpfVaccinationFirstDose = "01503398013",
                EmployeeCorenVaccinationFirstDose = "COREN456",
                HealthUnityCnpjFirstDose = "78466645000101",
                HealthUnityIneFirstDose = "987654321",
                HealthUnityCodeFirstDose = null,
                VaccineNameSecondDose = "Cominarty (Tozinameran/ BNT162b2)",
                VaccineManufacturerNameSecondDose = "Oxford/AstraZeneca/Fiocruz",
                BatchSecondDose = "789",
                VaccinationDateSecondDose = DateTime.Now,
                EmployeeNameVaccinationSecondDose = "Employee Name Dose Second",
                EmployeeCpfVaccinationSecondDose = "58230593027",
                EmployeeCorenVaccinationSecondDose = "COREN789",
                HealthUnityCnpjSecondDose = string.Empty,
                HealthUnityIneSecondDose = string.Empty,
                HealthUnityCodeSecondDose = 123456789,
                VaccineNameThirdDose = "Sputnik V (Gam-COVID-Vac) ",
                VaccineManufacturerNameThirdDose = "Instituto de Pesquisa Gamaleya/União Química",
                BatchThirdDose = "012",
                VaccinationDateThirdDose = DateTime.Now,
                EmployeeNameVaccinationThirdDose = "Employee Name Dose Third",
                EmployeeCpfVaccinationThirdDose = "08095935050",
                EmployeeCorenVaccinationThirdDose = "COREN021",
                HealthUnityCnpjThirdDose = string.Empty,
                HealthUnityIneThirdDose = string.Empty,
                HealthUnityCodeThirdDose = 987654321
            };

        public static UserImportDto UserImportDtoWithFullNameEmpty()
        {
            var user = UserImportDtoComplete();
            user.FullName = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithFullNameNull()
        {
            var user = UserImportDtoComplete();
            user.FullName = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithBrithdayHiggerThenActualDate()
        {
            var user = UserImportDtoComplete();
            user.Birthday = DateTime.Now.AddDays(1);
            return user;
        }

        public static UserImportDto UserImportDtoWithCpfNull()
        {
            var user = UserImportDtoComplete();
            user.Cpf = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithCpfEmpty()
        {
            var user = UserImportDtoComplete();
            user.Cpf = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithCpfLowLenght()
        {
            var user = UserImportDtoComplete();
            user.Cpf = user.Cpf[0..^1];
            return user;
        }
    }
}
