using System;

namespace iPassport.Application.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string CNS { get; set; }
        public string PassportDoc { get; set; }
    }
}