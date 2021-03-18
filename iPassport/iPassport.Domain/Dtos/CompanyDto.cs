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
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}