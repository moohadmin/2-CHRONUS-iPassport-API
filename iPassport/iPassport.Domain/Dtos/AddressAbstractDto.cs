using System;

namespace iPassport.Domain.Dtos
{
    public abstract class AddressAbstractDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CityId { get; set; }
        public string Cep { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
    }
}