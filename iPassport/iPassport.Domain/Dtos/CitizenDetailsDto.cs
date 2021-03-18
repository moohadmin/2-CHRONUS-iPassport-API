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
            NumberOfDoses = userDetails.UserVaccines?.Count();
            Telephone = authUser.PhoneNumber;
            Birthday = authUser.Birthday;
            WasTestPerformed = userDetails.UserDiseaseTests?.Any();
            Test = userDetails.UserDiseaseTests != null ? new UserDiseaseTestDto(userDetails.UserDiseaseTests?.OrderByDescending(x => x.CreateDate).FirstOrDefault()) : null;
            Address = authUser.Address != null ? new AddressDto(authUser.Address) : null;
            PriorityGroup = userDetails.PPriorityGroup != null ? new PriorityGroupDto(userDetails.PPriorityGroup) : null;
            BloodType = authUser.BBloodType != null ? new BloodTypeDto(authUser.BBloodType) : null;
            HumanRace = authUser.HumanRace != null ? new HumanRaceDto(authUser.HumanRace) : null;
            Company = authUser.Company != null ? new CompanyDto(authUser.Company) : null;
            Gender = authUser.GGender != null ? new GenderDto(authUser.GGender) : null;
            Doses = userDetails.UserVaccines?.GroupBy(v => new { v.VaccineId })
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
                    ExpirationDate = x.VaccinationDate.AddMonths(x.Vaccine?.ExpirationTimeInMonths ?? 0)
                })
            }).ToList();
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

        public AddressDto Address { get; set; }

        public IList<UserVaccineDetailsDto> Doses { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? WasTestPerformed { get; set; }

        public UserDiseaseTestDto Test { get; set; }
    }
}
