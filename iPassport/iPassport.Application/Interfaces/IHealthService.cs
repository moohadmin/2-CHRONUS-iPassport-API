using iPassport.Application.Models;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IHealthService
    {
        Task<ResponseApi> GetAll();
        Task<ResponseApi> SetHealthyAsync();
    }
}
