using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// User Disease Test Mapper Class
    /// </summary>
    public class UserDiseaseTestMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<UserDiseaseTest, UserDiseaseTestViewModel>()
                .ReverseMap();
        }
    }
}
