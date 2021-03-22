using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineManufacturerDto
    {
        public VaccineManufacturerDto() { }

        public VaccineManufacturerDto(VaccineManufacturer manufacturer)
        {
            Id = manufacturer?.Id;
            Name = manufacturer?.Name;
        }

        public Guid? Id { get; set; }
        public string Name { get;  set; }
    }
}