using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class StateDto
    {
        public StateDto(State state)
        {
            Id = state?.Id;
            Name = state?.Name;
            Acronym = state?.Acronym;
            Population = state?.Population;
            Country = new CountryDto(state?.Country);
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public int IbgeCode { get; set; }
        public int? Population { get; set; }

        public CountryDto Country { get; set; }
    }
}