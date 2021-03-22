using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// ImportedFileMapper Class
    /// </summary>
    public static class ImportedFileMapper
    {
        /// <summary>
        /// ImportedFileMapper Map Method
        /// </summary>
        /// <param name="profile"></param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<GetImportedFileRequest, GetImportedFileFilter>();

            profile.CreateMap<ImportedFile, ImportedFileViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.TotalLines, act => act.MapFrom(src => src.TotalLines))
                .ForMember(des => des.Date, act => act.MapFrom(src => src.CreateDate))
                .ForMember(des => des.Status, act => act.MapFrom(src => src.Status()))
                .ForMember(des => des.ImportedLines, act => act.MapFrom(src => src.TotalLinesImportedSuccessfully()))
                .ForMember(des => des.UnImportedLines, act => act.MapFrom(src => src.TotalLinesImportedError()))
                .ForMember(des => des.UserId, act => act.MapFrom(src => src.UserId))
                ;
                           
                
        }
    }
}
