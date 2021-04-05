using System;

namespace iPassport.Application.Models.ViewModels
{
    public class AdminDetailsViewModel
    {
        public Guid Id { get; set; }
        public string CompleteName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public CompanyViewModel Company { get; set; }
        public HealthUnitViewModel HealthUnit { get; set; }
        public ProfileViewModel Profile { get; set; }
        public string Occupation { get; set; }
        public bool? IsActive { get; set; }
    }
}
