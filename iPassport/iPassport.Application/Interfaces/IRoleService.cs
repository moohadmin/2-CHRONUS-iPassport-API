using iPassport.Application.Models;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseApi> Add(string role);
    }
}
