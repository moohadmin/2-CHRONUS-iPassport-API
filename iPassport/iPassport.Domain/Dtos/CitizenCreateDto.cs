using System;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos
{
    public class CitizenCreateDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Cpf { get; set; }
        public string Cns { get; set; }
        public string Rg { get; set; }
        public string PassportDocument { get; set; }
        public DateTime Birthday { get; set; }
        public Guid? CompanyId { get; set; }
        public string Occupation { get; set; }
        public string Bond { get; set; }
        public string PriorityGroup { get; set; }
        public string BloodType { get; set; }
        public string Breed { get; set; }
        public string Email { get; set; }
        public bool WasCovidInfected { get; set; }
        public int NumberOfDoses { get; set; }
        public string Telephone { get; set; }
        public AddressCreateDto Address { get; set; }
        public IList<UserVaccineCreateDto> Doses { get; set; }
    }
}
