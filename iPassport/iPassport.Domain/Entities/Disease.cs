using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Disease : Entity
    {
        public Disease() { }

        public Disease(string name, string description) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public Disease Create(DiseaseCreateDto dto) => new Disease(dto.Name, dto.Description);

        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual IEnumerable<Vaccine> Vaccines { get; set; }
        public virtual IEnumerable<UserDiseaseTest> UserDiseaseTests { get; set; }
    }
}
