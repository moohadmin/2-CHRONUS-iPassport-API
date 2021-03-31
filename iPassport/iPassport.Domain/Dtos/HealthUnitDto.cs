using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class HealthUnitDto
    {
        public HealthUnitDto(HealthUnit healthUnit)
        {
            Active = healthUnit.Active;
            Cnpj = healthUnit.Cnpj;
            Email = healthUnit.Email;
            Id = healthUnit.Id;
            Ine = healthUnit.Ine;
            Name = healthUnit.Name;
            ResponsiblePersonName = healthUnit.ResponsiblePersonName;
            ResponsiblePersonOccupation = healthUnit.ResponsiblePersonOccupation;
            ResponsiblePersonPhone = healthUnit.ResponsiblePersonPhone;
            Type = new HealthUnitTypeDto(healthUnit.Type);
            Address = new AddressDto() { Id = healthUnit.AddressId };
            Company = new CompanyDto() { Id = healthUnit.CompanyId };
        }

        public bool? Active { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Ine { get; set; }
        public string Name { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string ResponsiblePersonOccupation { get; set; }
        public string ResponsiblePersonPhone { get; set; }
        public HealthUnitTypeDto Type { get; set; }
        public AddressDto Address { get; set; }
        public CompanyDto Company { get; set; }
        public int? UniqueCode { get; set; }
    }
}
