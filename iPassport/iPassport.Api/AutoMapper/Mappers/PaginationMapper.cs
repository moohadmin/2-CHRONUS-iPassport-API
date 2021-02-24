using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    public class PaginationMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<PageFilterRequest, PageFilter>();
        }
    }
}
