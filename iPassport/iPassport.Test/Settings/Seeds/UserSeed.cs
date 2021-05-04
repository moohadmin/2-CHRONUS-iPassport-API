using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Test.Settings.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Test.Seeds
{
    public static class UserSeed
    {
        public static UserDetails GetUserDetails() =>
            new UserDetails(Guid.NewGuid())
            {
                Plan = PlanSeed.GetPlans().FirstOrDefault(),
                UserVaccines = new List<UserVaccine>()
                {
                    new UserVaccine(DateTime.UtcNow, 1, Guid.NewGuid(), Guid.NewGuid(), "test", "test", "test", "test", Guid.NewGuid())

                },
                UserDiseaseTests = new List<UserDiseaseTest>()
                {
                    new UserDiseaseTest(Guid.NewGuid(),null,DateTime.UtcNow,null,"PCR")
                },
                HealthUnit = new HealthUnit(Guid.NewGuid(), Guid.NewGuid())
            };

        public static Users GetUser() =>
            new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid());

        public static Users Get(EUserType userType)
        {
            var user = new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid());
            user.UserUserTypes = new List<UserUserType>()
            {
                new UserUserType(){ UserType = UserTypeSeed.Get(userType) }
            };
            return user;
        }

        public static Users GetUserAgent()
        {
            var user = new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid());
            user.UserUserTypes = new List<UserUserType>()
            {
                new UserUserType(){ UserType = UserTypeSeed.GetAgent() }
            };
            return user;
        }

        public static Users GetUserCitizen()
        {
            var user = new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid());
            user.UserUserTypes = new List<UserUserType>()
            {
                new UserUserType(){ UserType = UserTypeSeed.GetCitizen() }
            };
            return user;
        }

        public static Users GetUserAdmin()
        {
            var user = new Users("test", "test", "test", "test", Guid.NewGuid(), "Test", Guid.NewGuid());
            user.Profile = new("Administrativo", "admin");
            user.Company = CompanySeed.Get();            
            user.UserUserTypes = new List<UserUserType>()
            {
                new UserUserType(){ UserType = UserTypeSeed.GetAdmin() }
            };

            return user;
        }

        public static IList<Users> GetUsers()
        {
            return new List<Users>()
            {
                new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid()),
                new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid())

            };
        }

        public static PagedData<Users> GetPagedUsers()
        {
            return new PagedData<Users>() { Data = GetUsers() };
        }

        public static IList<ImportedUserDto> GetImportedUserDto() =>
            new List<ImportedUserDto>()
            {
                new()
                {
                    UserId = Guid.NewGuid(),
                    WasImported = false
                }
            };

        public static AdminDetailsViewModel GetAdminDetails() =>
            new AdminDetailsViewModel()
            {
                Id = Guid.NewGuid(),
                Company = new CompanyViewModel() { Id = Guid.NewGuid() },
                CompleteName = "teste",
                Cpf = "0000000000",
                Email = "teste@teste.com",
                HealthUnit = new HealthUnitViewModel() { Id = Guid.NewGuid() },
                IsActive = true,
                Occupation = "teste",
                Profile = new ProfileViewModel() { Id = Guid.NewGuid() },
                Telephone = "5571999999999"
            };

        public static PagedResponseApi GetPagedAdmins()
        {
            return new PagedResponseApi(
                true,
                "test",
                1,
                1,
                10,
                100,
                GetAdminUserViewModels()
            );
        }

        public static IList<AdminUserViewModel> GetAdminUserViewModels() =>
            new List<AdminUserViewModel>() {
                    new AdminUserViewModel()
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = "test",
                        CompleteName = "test",
                        Cpf = "00000000000",
                        IsActive = true,
                        ProfileName = "test",
                        Username = "test"
                    },
                    new AdminUserViewModel()
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = "test1",
                        CompleteName = "test1",
                        Cpf = "000000000001",
                        IsActive = true,
                        ProfileName = "test1",
                        Username = "test1"
                    },
                    new AdminUserViewModel()
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = "test2",
                        CompleteName = "test2",
                        Cpf = "000000000002",
                        IsActive = true,
                        ProfileName = "test2",
                        Username = "test2"
                    },
                };
    }
}
