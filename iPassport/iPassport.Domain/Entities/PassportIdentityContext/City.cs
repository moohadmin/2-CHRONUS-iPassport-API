using iPassport.Domain.Dtos;

namespace iPassport.Domain.Entities
{
    public class City : Entity
    {
        public City() { }
        public City(string name, string acronym, int ibgeCode, System.Guid stateId, int? population)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Acronym = acronym;
            IbgeCode = ibgeCode;
            StateId = stateId;
            Population = population;
        }

        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }
        public System.Guid StateId { get; private set; }
        public int? Population { get; private set; }

        public State State { get; set; }

        public City Create(CityCreateDto dto) => new City(dto.Name, dto.Acronym, dto.IbgeCode, dto.StateId, dto.Population);


    }
}
