using System;

namespace iPassport.Application.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string CNS { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string InternationalDocument { get; set; }
        public int profile { get; set; }
        public DateTime? LastLogin { get; set; }
        public PlanViewModel Plan { get; set; }
    }
}
