using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Application.Models.ViewModels
{
    public class UserVaccineViewModel
    {
        public Guid VaccineId { get; set; }
        public string VaccineName { get; set; }
        public IEnumerable<VaccineDoseViewModel> Doses { get; set; }
        public int RequiredDoses { get; set; }
        public int ImunizationTime { get; set; }
        public Guid UserId { get; set; }
        public EUserVaccineStatus Status { get; set; }
    }
}
