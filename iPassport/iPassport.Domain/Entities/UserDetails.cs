using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
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
        public bool IsImunized()
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

        public EUserVaccineStatus UserVaccineStatus(Guid vaccineId)
        {
            // Não tomou nenhuma dose
            if (UserVaccines == null || !UserVaccines.Any(x => x.VaccineId == vaccineId))
                return EUserVaccineStatus.NotVaccinated;

            var vaccine = UserVaccines.FirstOrDefault(x => x.VaccineId == vaccineId).Vaccine;
            var today = DateTime.UtcNow.Date;
            
            var dosesCount = UserVaccines.Count();
            var dosesDate = UserVaccines.Select(x => x.VaccinationDate).OrderBy(x => x).ToList();
            
            for (int i=0; i <= dosesDate.Count()-1; i++)
            {
                var maxdate = dosesDate[i].AddDays(vaccine.MaxTimeNextDose);
                var mindate = dosesDate[i].AddDays(vaccine.MinTimeNextDose);

                TimeSpan maxDifference = dosesDate[i + 1] - maxdate;
                TimeSpan minDifference = dosesDate[i + 1] - mindate;

                if (maxDifference.Days >= vaccine.MaxTimeNextDose && minDifference.Days <= vaccine.MinTimeNextDose)
                    return EUserVaccineStatus.NotVaccinated;

                if(dosesCount < vaccine.RequiredDoses)
                {
                    if(dosesDate.LastOrDefault())
                }
                else
                {

                }
            }

            return EUserVaccineStatus.NotVaccinated;
        }
    }
}
