using System;

namespace iPassport.Domain.Dtos
{
    public class AddressCreateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CityId { get; set; }
        public string Cep { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Complement { get; set; }
    }
}
