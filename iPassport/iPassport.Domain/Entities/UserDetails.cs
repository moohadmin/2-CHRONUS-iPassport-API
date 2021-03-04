using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, Guid? planId = null) : base()
        {
            UserId = userId;
            
            if (planId.HasValue)
                PlanId = planId.Value;
        }

        public Guid UserId { get; private set; }
        public Guid? PlanId { get; private set; }

        public virtual Plan Plan { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public UserDetails Create(UserCreateDto dto) => new UserDetails(dto.UserId);
        public void AssociatePlan(Guid plandId) => PlanId = plandId;
        public bool IsImmunized()
        {
            if (UserVaccines == null || !UserVaccines.Any())
                return false;

            var Vacinnes = UserVaccines.Select(x => x.Vaccine).Distinct();

            foreach (var vaccine in Vacinnes)
            {
                if(!UserVaccines.Any(x => x.Vaccine.Id == vaccine.Id && x.IsImmunized()))
                    return false;
            }

            return true;
        }


    }
}
