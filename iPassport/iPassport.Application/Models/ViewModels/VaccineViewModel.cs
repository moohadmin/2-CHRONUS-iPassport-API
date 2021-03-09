using System;

namespace iPassport.Application.Models.ViewModels
{
    public class VaccineViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RequiredDoses { get; set; }
        public int ExpirationTimeInMonths { get; set; }
        public int ImmunizationTimeInDays { get; set; }
        public Guid ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int MaxTimeNextDose { get; set; }
        public int MinTimeNextDose { get; set; }
    }
}
