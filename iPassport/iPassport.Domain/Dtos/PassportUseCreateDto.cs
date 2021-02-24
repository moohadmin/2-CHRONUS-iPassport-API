using System;

namespace iPassport.Domain.Dtos
{
    public class PassportUseCreateDto
    {
        public Guid AgentId { get; set; }
        public Guid CitizenId { get; set; }
        public Guid PassportDetailsId { get; set; }
        public bool AllowAccess { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
