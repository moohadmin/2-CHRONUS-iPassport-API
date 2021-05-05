using System;
using System.Collections.Generic;

namespace iPassport.Application.Models.ViewModels
{
    public class CitizenDetailsViewModel
    {
        public string CompleteName { get; set; }

        public GenderViewModel Gender { get; set; }

        public string Cpf { get; set; }

        public string Cns { get; set; }

        public CompanyViewModel Company { get; set; }

        public string Occupation { get; set; }

        public string Bond { get; set; }

        public PriorityGroupViewModel PriorityGroup { get; set; }

        public BloodTypeViewModel BloodType { get; set; }

        public HumanRaceViewModel HumanRace { get; set; }

        public string Email { get; set; }

        public bool? WasCovidInfected { get; set; }

        public int? NumberOfDoses { get; set; }

        public string Telephone { get; set; }

        public string Photo { get; set; }

        public AddressViewModel Address { get; set; }

        public IList<UserVaccineViewModel> Doses { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? WasTestPerformed { get; set; }

        public UserDiseaseTestViewModel Test { get; set; }

        public string ImportedFileName { get; set; }
        public DateTime? ImportedFileDate { get; set; }
    }
}
