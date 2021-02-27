using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using System;

namespace iPassport.Domain.Entities.Authentication
{
    public class Users : IdentityUser<Guid>
    {
        public Users() { }
        public Users(string fullName, string cpf, string rg, string cns, string passportDocument, DateTime birthday, string gender, string breed, string bloodType, string occupation, string address, string photo, string internationalDocument, string userName, string email, string mobile)
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
            Address = address;
            Photo = photo;
            InternationalDocument = internationalDocument;
            UserName = userName;
            Email = email;
            PhoneNumber = mobile;
        }

        public bool AcceptTerms { get; private set; }
        public DateTime UpdateDate { get; protected set; }
        public string FullName { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string CNS { get; private set; }
        public string PassportDoc { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime? LastLogin { get; set; }
        public string Gender { get; private set; }
        public string Breed { get; private set; }
        public string BloodType { get; private set; }
        public string Occupation { get; private set; }
        public string Address { get; private set; }
        public string Photo { get; private set; }
        public string InternationalDocument { get; private set; }

        public void SetAcceptTerms(bool acceptTerms) => AcceptTerms = acceptTerms;
        public void SetUpdateDate() => UpdateDate = DateTime.Now;
        public bool UserHavePhoto() => !string.IsNullOrWhiteSpace(Photo);
        public void UpdateLastLogin() => LastLogin = DateTime.Now;
        
        public void AddPhoto(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(Photo) && !string.IsNullOrWhiteSpace(imageUrl))
            {
                Photo = imageUrl;
            }
        }
 
        public void PhotoNameGenerator(UserImageDto dto)
        {
            dto.FileName = "ProfileImageUser_" + Id;
        }

        public Users Create(UserCreateDto dto) => new Users(dto.FullName, dto.CPF, dto.RG, dto.CNS, dto.Passport, dto.Birthday, dto.Gender, dto.Breed, dto.BloodType, dto.Occupation, dto.Address, dto.Photo, dto.InternationalDocument, dto.Username, dto.Email, dto.Mobile);
    }
}
