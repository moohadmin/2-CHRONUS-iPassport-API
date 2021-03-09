using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iPassport.Api.Models.Requests.User
{
    public class CitizenCreateRequest
    {
        public string CompleteName { get; set; }
        public EGendersTypes? Gender { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Cpf { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Cns { get; set; }
        public Guid? CompanyId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Occupation { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Bond { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string PriorityGroup { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string BloodType { get; set; }
        public EBreedTypes? Breed { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Email { get; set; }
        public bool? WasCovidInfected { get; set; }
        public int? NumberOfDoses { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Telephone { get; set; }
        public AddressCreateRequest Address { get; set; }
        public IList<UserVaccineCreateRequest> Doses {get; set;}
        public DateTime? Birthday { get; set; }
    }
}
