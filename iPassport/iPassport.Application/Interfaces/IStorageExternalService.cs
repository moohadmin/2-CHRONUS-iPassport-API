using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IStorageExternalService
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileName);
    }
}
