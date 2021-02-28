using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices
{
    public class ExternalStorageService : IExternalStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public ExternalStorageService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(UserImageDto userImageDto)
        {
            using (FileStream filestream = File.Create(InitDirectoryStorage() + userImageDto.FileName + (Path.GetExtension(userImageDto.ImageFile.FileName))))
            {
                await userImageDto.ImageFile.CopyToAsync(filestream);
                filestream.Flush();
                return "https://www.w3schools.com/howto/img_avatar.png";
                //return _configuration.GetSection("ContentStorage").GetSection("Image").GetSection("Path").Value + userImageDto.FileName + (Path.GetExtension(userImageDto.ImageFile.FileName));
            }
        }

        private string InitDirectoryStorage()
        {
            if (System.Convert.ToBoolean(_configuration.GetSection("ContentStorage").GetSection("Image").GetSection("Local").Value))
            {
                var directory = _configuration.GetSection("ContentStorage").GetSection("Image").GetSection("Path").Value;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return directory;
            }
            else
            {
                return "";
            }
        }
    }
}
