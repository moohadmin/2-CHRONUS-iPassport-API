using iPassport.Application.Models;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{

    public interface IPassportService
    {
        Task<ResponseApi> Get(string userId);
    }
}
