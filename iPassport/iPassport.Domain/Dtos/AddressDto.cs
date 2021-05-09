using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class AddressDto
    {
        public AddressDto(){}

        public AddressDto(Address address)
        {
            Id = address?.Id;
            Description = address?.Description;
            Cep = address?.Cep;
            Number = address?.Number;
            District = address?.District;
            City = new CityDto(address?.City);
            CityId = address?.CityId;
        }

        public Guid? Id { get; set; }
        public string Description { get; set; }
        public string Cep { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Complement { get; set; }

        public Guid? CityId { get; set; }

        public CityDto City { get; set; }
    }
}