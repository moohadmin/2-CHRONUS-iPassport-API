using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class HealthUnit : Entity
    {
        public HealthUnit() { }

        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public string Ine { get; private set; }
        public string Email { get; private set; }        
        public string ResponsiblePersonName { get; private set; }
        public string ResponsiblePersonPhone { get; private set; }
        public string ResponsiblePersonOccupation { get; private set; }
        public DateTime? DeactivationDate { get; private set; }
        public Guid TypeId { get; private set; }
        public Guid? AddressId { get; private set; }
        public bool? Active { get; private set; }

        public virtual HealthUnitType Type { get; set; }
        
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public HealthUnit Create(HealthUnitCreateDto dto) =>
            new HealthUnit()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Cnpj = dto.Cnpj,
                Ine = dto.Ine,
                Email = dto.Email,
                ResponsiblePersonName = dto.ResponsiblePersonName,
                ResponsiblePersonPhone = dto.ResponsiblePersonPhone,
                TypeId = (Guid)dto.TypeId,
                AddressId = dto.Address.Id,
                Active = dto.IsActive
            };
    }
}
