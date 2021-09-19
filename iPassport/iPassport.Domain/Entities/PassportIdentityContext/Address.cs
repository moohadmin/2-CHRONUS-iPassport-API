using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class Address : Entity
    {
        public Address() { }

        public Address(string description, Guid cityId, string cep, string number, string district, string complement) : base()
        {
            Id = Guid.NewGuid();
            Description = description;
            Cep = cep;
            CityId = cityId;
            Number = number;
            District = district;
            Complement = complement;
        }

        public Address(AddressDto dto)
        {
            Id = Guid.NewGuid();
            Description = dto.Description;
            Cep = dto.Cep;
            CityId = dto.CityId.Value;
            Number = dto.Number;
            District = dto.District;
        }

        public string Description { get; private set; }
        public string Cep { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public Guid CityId { get; private set; }
        public string Complement { get; private set; }
        
        public City City { get; set; }

        public Address Create(AddressCreateDto dto) => new Address(dto.Description, dto.CityId, dto.Cep, dto.Number, dto.District, dto.Complement);

        public void ChangeAddress(AddressEditDto dto)
        {
            Description = dto.Description;
            Cep = dto.Cep;
            CityId = dto.CityId;
            Number = dto.Number;
            District = dto.District;
            Complement = dto.Complement;
        }

        public void ChangeAddress(AddressDto dto)
        {
            Description = dto.Description;
            Cep = dto.Cep;
            CityId = dto.CityId.Value;
            Number = dto.Number;
            District = dto.District;
            Complement = dto.Complement;
        }
    }
}
