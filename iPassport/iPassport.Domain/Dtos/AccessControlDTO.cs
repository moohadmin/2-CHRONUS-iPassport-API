using System;

namespace iPassport.Domain.Dtos
{
    public class AccessControlDTO
    {
        public string Profile { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? HealthUnityId { get; set; }
        public Guid[] FilterIds { get; set; }
    }
}
