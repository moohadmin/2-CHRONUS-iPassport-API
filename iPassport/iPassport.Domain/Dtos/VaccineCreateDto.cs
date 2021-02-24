using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineCreateDto
    {
        public string Description { get; set; }
        public string Laboratory { get; set; }
        public int RequiredDoses { get; set; }
        public int ExpirationTime { get; set; }
        public int ImunizationTime { get; set; }
    }
}
