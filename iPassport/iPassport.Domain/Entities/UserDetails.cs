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
        
        public UserDetails Create(UserAgentCreateDto dto) => new UserDetails(dto.UserId);
        
        public UserDetails Create(Guid id) => new UserDetails(id);
        
        private IList<UserVaccine> CreateUservaccine(IList<UserVaccineCreateDto> dto)
        {
            var uservaccines = new List<UserVaccine>();

            foreach (var d in dto)
                uservaccines.Add(new UserVaccine().Create(d));

            return uservaccines;
        }

        public void AssociatePlan(Guid plandId) => PlanId = plandId;
        
        public bool IsImmunized()
        {
            if (UserVaccines == null || !UserVaccines.Any())
                return false;
            
            var vacinnes = UserVaccines.Select(x => x.Vaccine).Distinct().ToList();
            
            if (vacinnes == null || !vacinnes.Any())
                return false;
            
            foreach (var vaccine in vacinnes)
            {
                if (vaccine == null || GetUserVaccineStatus(vaccine.Id) != EUserVaccineStatus.Immunized)
                    return false;
            }
            return true;
        }

        public EUserVaccineStatus GetUserVaccineStatus(Guid vaccineId)
        {
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
                        return EUserVaccineStatus.NotImmunized;
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
