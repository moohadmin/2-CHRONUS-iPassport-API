using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class PassportUse : Entity
    {
        public PassportUse() { }
        public PassportUse(Guid agentId, Guid citizenId, Guid passportDetailsId, bool allowAccess, string latitude, string longitude)
        {
            Id = Guid.NewGuid();
            AgentId = agentId;
            CitizenId = citizenId;
            PassportDetailsId = passportDetailsId;
            AllowAccess = allowAccess;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid AgentId { get; private set; }
        public Guid CitizenId { get; private set; }
        public Guid PassportDetailsId { get; private set; }
        public bool AllowAccess { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }

        public UserDetails Agent { get; set; }
        public UserDetails Citizen { get; set; }
        public PassportDetails PassportDetails { get; set; }

        public PassportUse Create(PassportUseCreateDto dto) 
            => new PassportUse(dto.AgentId,dto.CitizenId,dto.PassportDetailsId,dto.AllowAccess,dto.Latitude,dto.Longitude);
    }
}


