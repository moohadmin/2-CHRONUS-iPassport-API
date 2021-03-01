using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineIndicatorDto
    {
        public Guid VaccnineId { get; set; }
        public string VaccineName { get; set; }
        public string Disease { get; set; }
        public Guid ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int Dose { get; set; }
        public bool UniqueDose { get; set; }
        public int Count { get; set; }
    }
}
