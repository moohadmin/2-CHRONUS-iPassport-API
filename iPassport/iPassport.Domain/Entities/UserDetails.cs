using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, Guid? planId = null) : base()
        {
            Id = userId;

            if (planId.HasValue)
                PlanId = planId.Value;
        }
        public Guid? PlanId { get; private set; }

        public virtual Plan Plan { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public UserDetails Create(UserCreateDto dto) => new UserDetails(dto.UserId);
        public void AssociatePlan(Guid plandId) => PlanId = plandId;
    }
}
