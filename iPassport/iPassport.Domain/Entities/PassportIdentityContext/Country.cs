using iPassport.Domain.Dtos;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Country : Entity
    {
        public Country() { }

        public Country(string name, string acronym, int ibgeCode) 
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Acronym = acronym;
            IbgeCode = ibgeCode;
        }

        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }

        public virtual IEnumerable<State> States { get; set; }

        public Country Create(CountryCreateDto dto) => new Country(dto.Name, dto.Acronym, dto.IbgeCode);
    }
}
