using System;

namespace iPassport.Domain.Dtos
{
    public class HealthUnitEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Ine { get; set; }
        public string Email { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string ResponsiblePersonLandline { get; set; }
        public string ResponsiblePersonMobilePhone { get; set; }
        public string ResponsiblePersonOccupation { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public Guid? TypeId { get; set; }
        public AddressEditDto Address { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
