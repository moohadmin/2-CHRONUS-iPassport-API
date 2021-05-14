using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Domain.Dtos
{
    public class CitizenDetailsDto
    {
        public CitizenDetailsDto(Users authUser, UserDetails userDetails)
        {
            CompleteName = authUser.FullName;
            Cpf = authUser.CPF;
            Cns = authUser.CNS;
            Occupation = authUser.Occupation;
            Bond = userDetails.Bond;
            Email = authUser.Email;
            WasCovidInfected = userDetails.WasCovidInfected;
            NumberOfDoses = userDetails.UserVaccines?.Where(x => x.ExclusionDate == null)?.Count();
            Telephone = authUser.PhoneNumber;
            Birthday = authUser.Birthday;
            Photo = authUser.Photo;
            WasTestPerformed = userDetails.WasTestPerformed;
            Test = userDetails.UserDiseaseTests.Any() ? new UserDiseaseTestDto(userDetails.UserDiseaseTests?.OrderByDescending(x => x.CreateDate).FirstOrDefault()) : null;
            Address = authUser.Address != null ? new AddressDto(authUser.Address) : null;
            PriorityGroup = userDetails.PriorityGroup != null ? new PriorityGroupDto(userDetails.PriorityGroup) : null;
            BloodType = authUser.BloodType != null ? new BloodTypeDto(authUser.BloodType) : null;
            HumanRace = authUser.HumanRace != null ? new HumanRaceDto(authUser.HumanRace) : null;
            Company = authUser.Company != null ? new CompanyDto(authUser.Company) : null;
            Gender = authUser.Gender != null ? new GenderDto(authUser.Gender) : null;
            Doses = userDetails.UserVaccines != null ? userDetails.UserVaccines?.Where(x => x.ExclusionDate == null)?.GroupBy(v => new { v.VaccineId })
            .Select(v => new UserVaccineDetailsDto()
            {
                UserId = v.FirstOrDefault().UserId,
                VaccineId = v.Key.VaccineId,
                VaccineName = v.FirstOrDefault().Vaccine?.Name,
                RequiredDoses = v.FirstOrDefault().Vaccine?.RequiredDoses,
                ImmunizationTime = v.FirstOrDefault().Vaccine?.ImmunizationTimeInDays,
                Doses = v.Select(x => new VaccineDoseDto()
                {
                    Id = x.Id,
                    Dose = x.Dose,
                    VaccinationDate = x.VaccinationDate,
                    ExpirationDate = x.VaccinationDate.AddMonths(x.Vaccine?.ExpirationTimeInMonths ?? 0),
                    Batch = x.Batch,
                    HealthUnit = x.HealthUnit != null ? new HealthUnitDto(x.HealthUnit) : null,
                    EmployeeName = x.EmployeeName,
                    EmployeeCpf = x.EmployeeCpf,
                    EmployeeCoren = x.EmployeeCoren
                }),
                Manufacturer = v.FirstOrDefault().Vaccine?.Manufacturer != null ? new VaccineManufacturerDto(v.FirstOrDefault().Vaccine.Manufacturer) : null
            }).ToList() : null;
            ImportedFileName = userDetails.ImportedFile?.Name;
            ImportedFileDate = userDetails.ImportedFile?.CreateDate;
            Rg = authUser.RG;
            InternationalDocument = authUser.InternationalDocument;
            PassportDocument = authUser.PassportDoc;
        }

        public string CompleteName { get; set; }

        public GenderDto Gender { get; set; }

        public string Cpf { get; set; }

        public string Cns { get; set; }

        public CompanyDto Company { get; set; }

        public string Occupation { get; set; }

        public string Bond { get; set; }

        public PriorityGroupDto PriorityGroup { get; set; }

        public BloodTypeDto BloodType { get; set; }

        public HumanRaceDto HumanRace { get; set; }

        public string Email { get; set; }

        public bool? WasCovidInfected { get; set; }

        public int? NumberOfDoses { get; set; }

        public string Telephone { get; set; }
        public string Photo { get; set; }
        public AddressDto Address { get; set; }

        public IList<UserVaccineDetailsDto> Doses { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? WasTestPerformed { get; set; }

        public UserDiseaseTestDto Test { get; set; }

        public string ImportedFileName { get; set; }
        
        public DateTime? ImportedFileDate { get; set; }
        
        public string Rg { get; set; }

        public string PassportDocument { get; set; }

        public string InternationalDocument { get; set; }
    }
}
