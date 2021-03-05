using System;

namespace iPassport.Application.Models.ViewModels
{
    public class VaccineDoseViewModel
    {
        public int Dose { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}