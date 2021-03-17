using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;

namespace iPassport.Domain.Entities.Authentication
{
    public class Users : IdentityUser<Guid>
    {
        public Users() { }
        public Users(string fullName, string cpf, string rg, string cns, string passportDocument, DateTime birthday, Guid? genderId, Guid? humanRaceId, Guid? bloodTypeId, string occupation, Address address, string photo, string internationalDocument, string userName, string email, string mobile, Guid? companyId, int profile)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CPF = cpf;
            RG = rg;
            CNS = cns;
            PassportDoc = passportDocument;
            Birthday = birthday;
            GenderId = genderId;
            HumanRaceId = humanRaceId;
            BloodTypeId = bloodTypeId;
            Occupation = occupation;
            Address = address;
            Photo = photo;
            InternationalDocument = internationalDocument;
            UserName = userName;
            Email = email;
            PhoneNumber = mobile;
            Profile = profile;
            CompanyId = companyId;
            CreateDate = DateTime.UtcNow;

            if (userName == null)
                UserName = Id.ToString();
        }

        public Users(string fullName, string cpf, Address address, string userName, string mobile, int profile, Guid? companyId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CPF = cpf;           
            Address = address;            
            UserName = userName;            
            PhoneNumber = mobile;
            Profile = profile;

            CompanyId = companyId;
            CreateDate = DateTime.UtcNow;
        }

        public bool AcceptTerms { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string CNS { get; set; }
        public string PassportDoc { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// Depreciated must use GenderId
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Depreciated must use HumanRaceId
        /// </summary>
        public string Breed { get; set; }
        /// <summary>
        /// Depreciated must use BloodTypeId
        /// </summary>
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public Guid? AddressId { get; private set; }
        public string Photo { get; set; }
        public string InternationalDocument { get; set; }
        public int Profile { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? HumanRaceId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? BloodTypeId { get; set; }
        

        public Address Address { get; set; }
        public Company Company { get; set; }
        public HumanRace HumanRace { get; set; }
        public Gender GGender { get; set; }
        public BloodType BBloodType { get; set; }


        public void SetAcceptTerms(bool acceptTerms) => AcceptTerms = acceptTerms;
        public void SetUpdateDate() => UpdateDate = DateTime.UtcNow;
        public bool UserHavePhoto() => !string.IsNullOrWhiteSpace(Photo);
        public void UpdateLastLogin() => LastLogin = DateTime.UtcNow;
        
        public void AddPhoto(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(Photo) && !string.IsNullOrWhiteSpace(imageUrl))
            {
                Photo = imageUrl;
            }
        }
 
        public void PhotoNameGenerator(UserImageDto dto)
        {
            var extension = Path.GetExtension(dto.ImageFile.FileName);
            dto.FileName = $"{Id}{extension}";
        }
                
        public Users CreateAgent(UserAgentCreateDto dto) =>
            new Users(dto.FullName, dto.CPF, dto.Address != null ? CreateUserAddress(dto.Address) : null, dto.Username, dto.Mobile, dto.Profile, dto.CompanyId);
        
        private Address CreateUserAddress(AddressCreateDto dto) =>
            new Address().Create(dto);

        public bool IsAgent() => Profile == (int)EProfileType.Agent;
        public bool IsCitizen() => Profile == (int)EProfileType.Citizen;
                
        public Users CreateCitizen(CitizenCreateDto dto)
        => new Users(dto.CompleteName,
                dto.Cpf,
                dto.Rg,
                dto.Cns,
                null,
                dto.Birthday,
                dto.GenderId,
                dto.HumanRaceId,
                dto.BloodTypeId, 
                dto.Occupation,
                CreateUserAddress(dto.Address),
                null,
                null,
                null,
                dto.Email,
                dto.Telephone,
                dto.CompanyId,
                (int)EProfileType.Citizen);
        
    }
}
