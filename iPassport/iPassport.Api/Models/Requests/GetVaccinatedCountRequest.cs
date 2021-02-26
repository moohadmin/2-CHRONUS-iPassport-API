using System;

namespace iPassport.Api.Models.Requests
{
    public class GetVaccinatedCountRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid DiseaseId { get; set; }
        public Guid? ManufacturerId { get; set; }
        public int DosageCount { get; set; }
    }
}
