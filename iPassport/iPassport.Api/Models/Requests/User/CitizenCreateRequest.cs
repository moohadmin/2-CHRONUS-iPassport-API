using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Api.Models.Requests.User
{
    public class CitizenCreateRequest
    {
        public string CompleteName { get; set; }
        public EGendersTypes? Gender { get; set; }
        public string Cpf { get; set; }
        public string Cns { get; set; }
        public Guid CompanyId { get; set; }
        public string Occupation { get; set; }
        public string Bond { get; set; }
        public string PriorityGroup { get; set; }
        public string BloodType { get; set; }
        public EBreedTypes? Breed { get; set; }
        public string Email { get; set; }
        public bool? WasCovidInfected { get; set; }
        public int? NumberOfDoses { get; set; }
        public string Telephone { get; set; }
        public AddressCreateRequest Address { get; set; }
        public IList<UserVaccineCreateRequest> Doses {get; set;}
        public DateTime? Birthday { get; set; }
    }
}
