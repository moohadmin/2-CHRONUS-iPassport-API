using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class CompanyDto
    {
        public CompanyDto(Company company)
        {
            Id = company?.Id;
            Name = company?.Name;
            AddressId = company?.AddressId;
            Cnpj = company?.Cnpj;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? AddressId { get; set; }
        public string Cnpj { get; set; }
    }
}