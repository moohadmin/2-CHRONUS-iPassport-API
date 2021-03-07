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
        public Users(string fullName, string cpf, string rg, string cns, string passportDocument, DateTime birthday, string gender, string breed, string bloodType, string occupation, string address, string photo, string internationalDocument, string userName, string email, string mobile, int profile)
        {
            Id = Guid.NewGuid();
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
            //Address = address;
            Photo = photo;
            InternationalDocument = internationalDocument;
            UserName = userName;
            Email = email;
            PhoneNumber = mobile;
            Profile = profile;
        }

        public Users(string fullName, string cpf, AddressCreateDto address, string userName, string mobile, int profile, Guid companyId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CPF = cpf;           
            Address = CreateUserAddress(address);            
            UserName = userName;            
            PhoneNumber = mobile;
            Profile = profile;
            CompanyId = companyId;
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
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public Guid AddressId { get; private set; }
        public string Photo { get; set; }
        public string InternationalDocument { get; set; }
        public int Profile { get; set; }
        public Guid? CompanyId { get; set; }


        public Address Address { get; set; }
        public Company Company { get; set; }


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
                
        //public Users Create(UserCreateDto dto) => new Users(dto.FullName, dto.CPF, dto.RG, dto.CNS, dto.Passport, dto.Birthday, dto.Gender, dto.Breed, dto.BloodType, dto.Occupation, dto.Address, dto.Photo, dto.InternationalDocument, dto.Username, dto.Email, dto.Mobile, dto.Profile);
        public Users CreateAgent(UserAgentCreateDto dto) => new Users(dto.FullName,dto.CPF,dto.Address,dto.Username,dto.Mobile,dto.Profile,dto.CompanyId);
        private Address CreateUserAddress(AddressCreateDto dto) => new Address().Create(dto);

        public bool IsAgent() => Profile == (int)EProfileType.Agent;
        public bool IsCitizen() => Profile == (int)EProfileType.Citizen;
    }
}
