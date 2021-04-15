using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class CompanyAssociatedDto
    {
        public CompanyAssociatedDto() {}
        public CompanyAssociatedDto(Company company, bool canAssociate)
        {
            Id = company.Id;
            Name = company.Name;
            AddressId = company.AddressId;
            Cnpj = company.Cnpj;
            CanAssociate = canAssociate;
            TradeName = company.TradeName;
            Address = company.Address == null ? null : new AddressDto(company.Address);
            Segment = company.Segment == null ? null : new CompanySegmentDto(company.Segment);
            Active = !company.DeactivationDate.HasValue;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? AddressId { get; set; }
        public string Cnpj { get; set; }
        public bool CanAssociate { get; set; }        
        public string TradeName { get; set; }
        public AddressDto Address { get; set; }
        public CompanySegmentDto Segment { get; set; }
        public bool Active { get; set; }
    }
}