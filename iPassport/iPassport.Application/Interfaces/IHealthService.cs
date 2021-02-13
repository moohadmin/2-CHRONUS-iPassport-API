using iPassport.Application.Models;
using iPassport.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IHealthService
    {
        Task<ResponseApi> GetAll();
        Task<ResponseApi> SetHealthyAsync();
    }
}
