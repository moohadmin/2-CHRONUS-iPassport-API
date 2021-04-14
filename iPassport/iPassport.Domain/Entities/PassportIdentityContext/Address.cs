using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class Address : Entity
    {
        public Address() { }

        public Address(string description, Guid cityId, string cep, string number, string district) : base()
        {
            Id = Guid.NewGuid();
            Description = description;
            Cep = cep;
            CityId = cityId;
            Number = number;
            District = district;
        }

        public string Description { get; private set; }
        public string Cep { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public Guid CityId { get; private set; }

        public City City { get; set; }

        public Address Create(AddressAbstractDto dto) => new Address(dto.Description, dto.CityId, dto.Cep, dto.Number, dto.District);

        public void ChangeAddress(AddressAbstractDto dto)
        {
            Description = dto.Description;
            Cep = dto.Cep;
            CityId = dto.CityId;
            Number = dto.Number;
            District = dto.District;
        }
    }
}
