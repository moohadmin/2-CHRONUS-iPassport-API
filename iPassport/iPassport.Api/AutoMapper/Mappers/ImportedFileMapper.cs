using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using AutoM = AutoMapper;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Imported File Mapper Class
    /// </summary>
    public static class ImportedFileMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
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
                .ForMember(des => des.UserId, act => act.MapFrom(src => src.UserId));

            profile.CreateMap<ImportedFileDetails, ImportedFileDetailsViewModel>();
        }
    }
}
