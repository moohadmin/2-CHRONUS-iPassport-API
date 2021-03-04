using iPassport.Domain.Dtos;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class State : Entity
    {
        public State() { }

        public State(string name, string acronym, int ibgeCode, System.Guid countryId)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Acronym = acronym;
            IbgeCode = ibgeCode;
            CountryId = countryId;
        }

        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }
        public System.Guid CountryId { get; private set; }

        public Country Country { get; set; }

        public virtual IEnumerable<City> Cities { get; set; }

        public State Create(StateCreateDto dto) => new State(dto.Name, dto.Acronym, dto.IbgeCode, dto.CountryId);
            


    }
}
