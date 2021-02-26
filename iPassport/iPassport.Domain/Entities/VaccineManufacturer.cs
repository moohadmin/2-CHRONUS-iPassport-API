using iPassport.Domain.Dtos;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class VaccineManufacturer : Entity
    {
        public VaccineManufacturer() { }
        public VaccineManufacturer(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public virtual IEnumerable<Vaccine> Vaccines { get; set; }

        public VaccineManufacturer Create(VaccineManufacturerCreateDto dto) => new VaccineManufacturer(dto.Name);
    }
}
