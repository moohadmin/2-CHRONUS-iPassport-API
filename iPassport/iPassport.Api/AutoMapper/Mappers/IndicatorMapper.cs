using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Indicator Mapper Class
    /// </summary>
    public static class IndicatorMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<GetVaccinatedCountRequest, GetVaccinatedCountFilter>();
        }
    }
}
