using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, string fullName, string cpf, string rg, string cns, string passportDocument, DateTime birthday, string gender, string breed, string bloodType, string occupation, string address, string photo, string internationalDocument, Guid? planId = null) : base()
        {
            UserId = userId;
            FullName = fullName;
            CPF = cpf;
            RG = rg;
            CNS = cns;
            PassportDoc = passportDocument;
            Birthday = birthday;
            Gender = gender;
            Breed = breed;
            BloodType = bloodType;
            Occupation = occupation;
            Address = address;
            Photo = photo;
            InternationalDocument = internationalDocument;
            
            if (planId.HasValue)
                PlanId = planId.Value;
        }


        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string CNS { get; private set; }
        /// <summary>
        /// Document Passport Description
        /// </summary>
        public string PassportDoc { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime? LastLogin { get; set; }
        public string Gender { get; private set; }
        public string Breed { get; private set; }
        public string BloodType { get; private set; }
        public string Occupation { get; private set; }
        public string Address { get; private set; }
        public string Photo { get; private set; }
        public Guid? PlanId { get; private set; }
        public string InternationalDocument { get; private set; }

        public virtual Plan Plan { get; set; }
        /// <summary>
        /// Entity Passport
        /// </summary>
        public virtual Passport Passport { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }
        public int Profile { get; set; }

        public UserDetails Create(UserCreateDto dto) => new UserDetails(dto.UserId, dto.FullName, dto.CPF, dto.RG, dto.CNS, dto.Passport, dto.Birthday, dto.Gender, dto.Breed, dto.BloodType, dto.Occupation, dto.Address, dto.Photo, dto.InternationalDocument);

        public void AddPhoto(string imageUrl)
        {
            if (String.IsNullOrWhiteSpace(Photo) && !string.IsNullOrWhiteSpace(imageUrl))
            {
                Photo = imageUrl;
            }
        }

        public void PhotoNameGenerator(UserImageDto dto)
        {
            dto.FileName = "ProfileImageUserId_" + UserId;
        }

        public bool UserHavePhoto() => !String.IsNullOrWhiteSpace(Photo);


        public void AssociatePlan(Guid plandId) => PlanId = plandId;
        public void UpdateLastLogin() => LastLogin = DateTime.Now;
    }
}
