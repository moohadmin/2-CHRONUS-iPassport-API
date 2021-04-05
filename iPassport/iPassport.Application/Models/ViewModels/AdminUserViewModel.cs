using System;

namespace iPassport.Application.Models.ViewModels
{
    public class AdminUserViewModel
    {
        public Guid Id { get; set; }
        public string CompleteName { get; set; }
        public string Cpf { get; set; }
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public string ProfileName { get; set; }
        public bool IsActive { get; set; }
    }
}
