using System;

namespace iPassport.Application.Models.ViewModels
{
    public class VaccineDoseViewModel
    {
        public Guid Id { get; set; }
        public int Dose { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}