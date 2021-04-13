using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class CompanyResponsible : Entity
    {
        public CompanyResponsible() { }
        public CompanyResponsible(Guid companyId, string name, string occupation, string email, string mobilePhone, string landline)
        {
            Id = companyId;
            Name = name;
            Occupation = occupation;
            Email = email;
            MobilePhone = mobilePhone;
            Landline = landline;
        }

        public string Name { get; private set; }
        public string Occupation { get; private set; }
        public string Email { get; private set; }
        public string MobilePhone { get; private set; }
        public string Landline { get; private set; }
        public Company Company { get; set; }

        public static CompanyResponsible Create(CompanyResponsibleCreateDto dto) =>
            new(dto.CompanyId.Value
                , dto.Name
                , dto.Occupation
                , dto.Email
                , dto.MobilePhone
                , dto.Landline);
    }
}
