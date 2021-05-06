using iPassport.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IStorageExternalService
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileName);
        Task<string> GeneratePreSignedURL(string filename, EImageSize? size);
    }
}
