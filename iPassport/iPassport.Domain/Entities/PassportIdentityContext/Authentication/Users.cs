using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iPassport.Domain.Entities.Authentication
{
    public class Users : IdentityUser<Guid>
    {
        public Users() { }
        /// <summary>
        /// User Citizen
        /// </summary>
        public Users(string fullName, string cpf, string rg, string cns, string passportDocument, DateTime birthday, Guid? genderId, Guid? humanRaceId, Guid? bloodTypeId, string occupation, Address address, string photo, string internationalDocument, string userName, string email, string mobile, Guid? companyId, int userType)
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
            UserType = userType;
            CompanyId = companyId;
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;

            if (userName == null)
                UserName = Id.ToString();

            
        }
        /// <summary>
        /// User Agent
        /// </summary>
        public Users(string fullName, string cpf, Address address, string userName, string mobile, int userType, Guid? companyId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CPF = cpf;
            Address = address;
            UserName = userName;
            PhoneNumber = mobile;
            UserType = userType;

            CompanyId = companyId;
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
        /// <summary>
        /// User Admin
        /// </summary>
        public Users(string fullName, string cpf, string email, string mobile, Guid companyId,
                        string occupation, Guid? profileId, int userType)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CPF = cpf;
            Email = email;
            PhoneNumber = mobile;
            CompanyId = companyId;
            Occupation = occupation;
            if (profileId.HasValue)
                ProfileId = profileId;
            UserType = userType;
            UserName = Id.ToString();

            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
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
        public Guid? AddressId { get; set; }
        public string Photo { get; set; }
        public string InternationalDocument { get; set; }
        public int UserType { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? HumanRaceId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? BloodTypeId { get; set; }
        public Guid? ProfileId { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public Guid? DeactivationUserId { get; set; }
        public string CorporateCellphoneNumber { get; set; }

        public Address Address { get; set; }
        public Company Company { get; set; }
        public HumanRace HumanRace { get; set; }
        public Gender GGender { get; set; }
        public BloodType BBloodType { get; set; }
        public Profile Profile { get; set; }
        public Users DeactivationUser { get; set; }

        public virtual IList<UserUserType> UserUserTypes { get; set; }

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
            new Users(dto.FullName, dto.CPF, dto.Address != null ? CreateUserAddress(dto.Address) : null, dto.Username, dto.Mobile, (int)EUserType.Agent, dto.CompanyId);

        private Address CreateUserAddress(AddressCreateDto dto) =>
            new Address().Create(dto);

        public bool IsAgent() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsAgent());
        public bool IsInactiveAgent() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsAgent() && x.IsInactive());

        public bool IsCitizen() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsCitizen());
        public bool IsInactiveCitizen() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsCitizen() && x.IsInactive());

        public bool IsAdminType() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsAdmin());
        public bool IsInactiveAdminType() => UserUserTypes != null && UserUserTypes.Any(x => x.UserType.IsAdmin() && x.IsInactive());
        
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
                (int)EUserType.Citizen);

        public void ChangeCitizen(CitizenEditDto dto)
        {
            SetUpdateDate();
            Birthday = dto.Birthday;
            CNS = dto.Cns;
            FullName = dto.CompleteName;
            CPF = dto.Cpf;
            Email = dto.Email;
            Occupation = dto.Occupation;
            PhoneNumber = dto.Telephone;

            CompanyId = dto.CompanyId;
            BloodTypeId = dto.BloodTypeId;
            GenderId = dto.GenderId;
            HumanRaceId = dto.HumanRaceId;

            if (AddressId.HasValue)
                Address.ChangeAddress(dto.Address);
        }

        public static Users CreateCitizen(UserImportDto dto)
            => new(dto.FullName,
                    dto.Cpf,
                    null,
                    dto.Cns,
                    null,
                    dto.Birthday,
                    dto.GenderId,
                    dto.HumanRaceId,
                    dto.BloodTypeId,
                    dto.Occupation,
                    new Address(dto.Address, dto.CityId, dto.Cep, dto.Number, dto.District),
                    null,
                    null,
                    null,
                    dto.Email,
                    string.Concat(dto.CountryCode, dto.PhoneNumber),
                    dto.CompanyId,
                    (int)EUserType.Citizen);

        public static Users CreateUser(AdminDto dto, Guid userTypeId)
        {
            var user = new Users(dto.CompleteName
                , dto.Cpf
                , dto.Email
                , dto.Telephone
                , dto.CompanyId.Value
                , dto.Occupation
                , dto.ProfileId
                , (int)EUserType.Admin);

            user.AddUserType(userTypeId);

            return user;
        }
            

        public void Deactivate(Guid deactivationUserId, EUserType? userTypeIdentifyer = null)
        {
            if(userTypeIdentifyer == null)
            {
                DeactivationUserId = deactivationUserId;
                DeactivationDate = DateTime.UtcNow;
            }else if (UserUserTypes != null)
                UserUserTypes.FirstOrDefault(x => x.UserType.Identifyer == (int)userTypeIdentifyer).Deactivate(deactivationUserId);            
        }

        public void ChangeUser(AdminDto dto)
        {
            SetUpdateDate();
            FullName = dto.CompleteName;
            CPF = dto.Cpf;
            Email = dto.Email;
            PhoneNumber = dto.Telephone;
            CompanyId = dto.CompanyId;
            Occupation = dto.Occupation;
            ProfileId = dto.ProfileId;
        }

        public bool IsActive() => !DeactivationDate.HasValue;
        public bool IsInactive() => DeactivationDate.HasValue;
        public void Activate()
        {
            DeactivationUserId = null;
            DeactivationDate = null;
        }

        public bool CanEditCitizenFields(CitizenEditDto dto, AccessControlDTO accessControl)
        {
            if (accessControl.Profile == EProfileKey.government.ToString())
                return Birthday.Date == dto.Birthday.Date
                     && CNS == dto.Cns
                     && CPF == dto.Cpf
                     && Email == dto.Email
                     && Occupation == dto.Occupation
                     && PhoneNumber == dto.Telephone
                     && CompanyId == dto.CompanyId;

            if (accessControl.Profile == EProfileKey.healthUnit.ToString())
                return Birthday.Date == dto.Birthday.Date
                    && CNS == dto.Cns
                    && FullName == dto.CompleteName
                    && CPF == dto.Cpf
                    && Email == dto.Email
                    && Occupation == dto.Occupation
                    && PhoneNumber == dto.Telephone
                    && CompanyId == dto.CompanyId
                    && BloodTypeId == dto.BloodTypeId
                    && GenderId == dto.GenderId
                    && HumanRaceId == dto.HumanRaceId
                    && Address.Number == dto.Address.Number
                    && Address.Cep == dto.Address.Cep
                    && Address.CityId == dto.Address.CityId
                    && Address.Description == dto.Address.Description
                    && Address.District == dto.Address.District;
            
            return accessControl.Profile == EProfileKey.admin.ToString();
        }

        private void AddUserType(Guid userTypeId)
        {
            var userUserType = new UserUserType(Id, userTypeId);

            if (UserUserTypes == null)
                UserUserTypes = new List<UserUserType>();

            UserUserTypes.Add(userUserType);
        }
    }
}

