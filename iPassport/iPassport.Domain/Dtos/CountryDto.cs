using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class CountryDto
    {
        public CountryDto(Country country)
        {
            Id = country?.Id;
            Name = country?.Name;
            Acronym = country?.Acronym;
            ExternalCode = country?.ExternalCode;
            Population = country?.Population;
        }

        public Guid? Id { get; set; } 
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string ExternalCode { get; set; }
        public long? Population { get; set; }
    }
}