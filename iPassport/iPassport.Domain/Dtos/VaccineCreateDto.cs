using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineCreateDto
    {
        public string Name { get; set; }
        public Guid ManufacturerId { get; set; }
        public int RequiredDoses { get; set; }
        public int ExpirationTime { get; set; }
        public int ImunizationTime { get; set; }
        public bool UniqueDose { get; set; }
        public int MaxTimeNextDose { get; private set; }
        public int MinTimeNextDose { get; private set; }
    }
}
