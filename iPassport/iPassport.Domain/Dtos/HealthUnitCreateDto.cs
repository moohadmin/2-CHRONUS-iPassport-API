using System;

namespace iPassport.Domain.Dtos
{
    public class HealthUnitCreateDto
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Ine { get; set; }
        public string Email { get; set; }
        public string ResponsiblePersonName { get; set; }
        public string ResponsiblePersonPhone { get; set; }
        public string ResponsiblePersonOccupation { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public Guid? TypeId { get; set; }
        public AddressCreateDto Address { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
