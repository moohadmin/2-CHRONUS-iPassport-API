using iPassport.Domain.Dtos;

namespace iPassport.Domain.Entities
{
    public class Address : Entity
    {
        public Address() { }
        public Address(string description, System.Guid cityId, string cep) : base()
        {
            Id = System.Guid.NewGuid();
            Description = description;
            Cep = cep;
            CityId = cityId;
        }

        public string Description { get; private set; }
        public string Cep { get; private set; }
        public System.Guid CityId { get; private set; }
        public City City { get; set; }

        public Address Create(AddressCreateDto dto) => new Address(dto.Description, dto.CityId, dto.Cep);


    }
}
