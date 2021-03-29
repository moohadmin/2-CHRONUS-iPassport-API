using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class Address : Entity
    {
        public Address() { }

        public Address(string description, System.Guid cityId, string cep, string number, string district) : base()
        {
            Id = System.Guid.NewGuid();
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
        public System.Guid CityId { get; private set; }
        public City City { get; set; }

        public Address Create(AddressCreateDto dto) => new Address(dto.Description, dto.CityId, dto.Cep, dto.Number, dto.District);

        public void ChangeAddress(AddressEditDto dto)
        {
            Description = dto.Description;
            Cep = dto.Cep;
            CityId = dto.CityId;
            Number = dto.Number;
            District = dto.District;
        }
    }
}
