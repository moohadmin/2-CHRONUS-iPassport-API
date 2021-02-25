using iPassport.Domain.Dtos;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IExternalStorageService
    {
        Task<string> UploadFileAsync(UserImageDto userImageDto);
    }
}
