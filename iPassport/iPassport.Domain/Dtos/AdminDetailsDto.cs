using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using System;

namespace iPassport.Domain.Dtos
{
    public class AdminDetailsDto
    {
        public AdminDetailsDto(Users authUser, UserDetails details = null)
        {
            Id = authUser.Id;
            CompleteName = authUser.FullName;
            Cpf = authUser.CPF;
            Email = authUser.Email;
            Telephone = authUser.PhoneNumber;
            Company = authUser.Company != null ? new CompanyDto(authUser.Company) : null;
            HealthUnit = details?.HealthUnit != null ? new HealthUnitDto(details.HealthUnit) : null;
            Occupation = authUser.Occupation;
            Profile = authUser.Profile != null ? new ProfileDto(authUser.Profile) : null;
            IsActive = !authUser.IsInactiveAdminType();
        }

        public Guid Id { get; set; }
        public string CompleteName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public CompanyDto Company { get; set; }
        public HealthUnitDto HealthUnit { get; set; }
        public string Occupation { get; set; }
        public ProfileDto Profile { get; set; }
        public bool? IsActive { get; set; }
    }
}
