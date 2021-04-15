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
                PhoneNumber = 71912345678,
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

        public static UserImportDto UserImportDtoWithCpfAndNulls()
        {
            var user = UserImportDtoComplete();
            user.Cpf = null;
            user.Cns = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithCpfAndCnsEmpty()
        {
            var user = UserImportDtoComplete();
            user.Cpf = string.Empty;
            user.Cns = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithCpfLowLenght()
        {
            var user = UserImportDtoComplete();
            user.Cpf = user.Cpf[0..^1];
            return user;
        }

        public static UserImportDto UserImportDtoWithCnsLowLenght()
        {
            var user = UserImportDtoComplete();
            user.Cns = user.Cns[0..^1];
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

        public static UserImportDto UserImportDtoWithCnsNull()
        {
            var user = UserImportDtoComplete();
            user.Cns = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithCnsEmpty()
        {
            var user = UserImportDtoComplete();
            user.Cns = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithPhoneNumberLowLenght()
        {
            var user = UserImportDtoComplete();
            user.PhoneNumber = 719123456;
            return user;
        }

        public static UserImportDto UserImportDtoWithInvalidEmail()
        {
            var user = UserImportDtoComplete();
            user.Email = "emailTest";
            return user;
        }

        public static UserImportDto UserImportDtoWithCepLowLenght()
        {
            var user = UserImportDtoComplete();
            user.Cep = "123456";
            return user;
        }

        public static UserImportDto UserImportDtoWithCityNull()
        {
            var user = UserImportDtoComplete();
            user.City = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithCityEmpty()
        {
            var user = UserImportDtoComplete();
            user.City = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithStateNull()
        {
            var user = UserImportDtoComplete();
            user.State = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithStateEmpty()
        {
            var user = UserImportDtoComplete();
            user.State = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithCountryNull()
        {
            var user = UserImportDtoComplete();
            user.Country = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithCountryEmpty()
        {
            var user = UserImportDtoComplete();
            user.Country = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithWasCovidInfectedNull()
        {
            var user = UserImportDtoComplete();
            user.WasCovidInfected = null;
            return user;
        }

        public static UserImportDto UserImportDtoWithWasCovidInfectedEmpty()
        {
            var user = UserImportDtoComplete();
            user.WasCovidInfected = string.Empty;
            return user;
        }

        public static UserImportDto UserImportDtoWithWasCovidInfectedInvalidValue()
        {
            var user = UserImportDtoComplete();
            user.WasCovidInfected = "TESTE";
            return user;
        }

        internal static UserImportDto UserImportDtoWithWasTestPerformedNull()
        {
            var user = UserImportDtoComplete();
            user.WasTestPerformed = null;
            return user;
        }

        internal static UserImportDto UserImportDtoWithWasTestPerformedEmpty()
        {
            var user = UserImportDtoComplete();
            user.WasTestPerformed = string.Empty;
            return user;
        }

        internal static UserImportDto UserImportDtoWithWasTestPerformedInvalidValue()
        {
            var user = UserImportDtoComplete();
            user.WasTestPerformed = "TESTE";
            return user;
        }

        internal static UserImportDto UserImportDtoWithTestDateNullAndWasTestPerformed()
        {
            var user = UserImportDtoComplete();
            user.WasTestPerformed = "Sim";
            user.TestDate = null;
            return user;
        }

        internal static UserImportDto UserImportDtoWithTestDateHigherThenCurrentDate()
        {
            var user = UserImportDtoComplete();
            user.TestDate = DateTime.UtcNow.AddDays(1);
            return user;
        }

        internal static UserImportDto UserImportDtoWithResultNull()
        {
            var user = UserImportDtoComplete();
            user.Result = null;
            return user;
        }

        internal static UserImportDto UserImportDtoWithResultEmpty()
        {
            var user = UserImportDtoComplete();
            user.Result = string.Empty;
            return user;
        }

        internal static UserImportDto UserImportDtoWithResultNonstandardValue()
        {
            var user = UserImportDtoComplete();
            user.Result = "TESTE";
            return user;
        }

        internal static UserImportDto UserImportDtoWithResultDateNullAndResultHasValue()
        {
            var user = UserImportDtoComplete();
            user.ResultDate = null;
            return user;
        }

        internal static UserImportDto UserImportDtoWithResultDateHigherThenCurrentDate()
        {
            var user = UserImportDtoComplete();
            user.ResultDate = DateTime.UtcNow.AddDays(1);
            return user;
        }
    }
}
