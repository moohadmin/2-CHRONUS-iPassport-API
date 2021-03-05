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

            var vacinnes = UserVaccines.Select(x => x.Vaccine).Distinct().ToList();
            
            foreach (var vaccine in vacinnes)
            {
                if(vaccine == null || !UserVaccines.Any(x => x.VaccineId == vaccine.Id && x.IsImmunized()))
                    return false;
            }

            return true;
        }

        public EUserVaccineStatus GetUserVaccineStatus(Guid vaccineId)
        {
            // Não tomou nenhuma dose
            if (UserVaccines == null || !UserVaccines.Any(x => x.VaccineId == vaccineId))
                return EUserVaccineStatus.Unvaccinated;

            var vaccine = UserVaccines.FirstOrDefault(x => x.VaccineId == vaccineId).Vaccine;
            
            DateTime lastVaccineDate = DateTime.MinValue;
            int validDoses = 0;

            foreach (var userVaccine in UserVaccines.Where(x => x.VaccineId == vaccineId).OrderBy(x => x.Dose))
            {
                if (vaccine.UniqueDose())
                    return userVaccine.IsImmunized() ? EUserVaccineStatus.Immunized : EUserVaccineStatus.Vaccinated;
               
                else if (!userVaccine.IsFirstDose())
                {
                    if (userVaccine.VaccinationDate >= lastVaccineDate.AddDays(vaccine.MinTimeNextDose) 
                        && userVaccine.VaccinationDate <= lastVaccineDate.AddDays(vaccine.MaxTimeNextDose))
                        validDoses += 1;
                    else
                        return EUserVaccineStatus.Unvaccinated;
                }
                else
                    validDoses += 1;
                
                lastVaccineDate = userVaccine.VaccinationDate;
            }

            if (validDoses == vaccine.RequiredDoses)
            {
                var lastDose = UserVaccines.FirstOrDefault(x => x.VaccineId == vaccineId && x.Dose == vaccine.RequiredDoses);

                return lastDose.IsImmunized() ? EUserVaccineStatus.Immunized : EUserVaccineStatus.Vaccinated;
            }
            else
                return EUserVaccineStatus.Waiting;
        }
    }
}
