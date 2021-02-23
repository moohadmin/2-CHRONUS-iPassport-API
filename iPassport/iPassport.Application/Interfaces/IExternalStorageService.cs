using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IExternalStorageService
    {        
        Task<string> UploadFileAsync(UserImageDto userImageDto);
    }
}
