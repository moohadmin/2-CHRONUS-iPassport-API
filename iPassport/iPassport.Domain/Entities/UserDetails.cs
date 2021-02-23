using iPassport.Domain.Dtos;
using System;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, string fullName, string cpf, string rg, string cns, string passport, DateTime birthday, string gender, string breed, string bloodType, string occupation, string address, string photo, Guid? planId = null) : base()
        {
            UserId = userId;
            FullName = fullName;
            CPF = cpf;
            RG = rg;
            CNS = cns;
            Passport = passport;
            Birthday = birthday;
            Gender = gender;
            Breed = breed;
            BloodType = bloodType;
            Occupation = occupation;
            Address = address;
            Photo = photo;
            
            if (planId.HasValue)
                PlanId = planId.Value;
        }


        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string CNS { get; private set; }
        public string Passport { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime? LastLogin { get; set; }
        public string Gender { get; private set; }
        public string Breed { get; private set; }
        public string BloodType { get; private set; }
        public string Occupation { get; private set; }
        public string Address { get; private set; }
        public string Photo { get; private set; }
        public Guid? PlanId { get; private set; }

        public virtual Plan Plan { get; set; }

        public UserDetails Create(UserCreateDto dto) => new UserDetails(dto.UserId, dto.FullName, dto.CPF, dto.RG, dto.CNS, dto.Passport, dto.Birthday, dto.Gender, dto.Breed, dto.BloodType, dto.Occupation, dto.Address, dto.Photo);
        public void AssociatePlan(Guid plandId) => PlanId = plandId;
        public void UpdateLastLogin() => LastLogin = DateTime.Now;
    }
}
