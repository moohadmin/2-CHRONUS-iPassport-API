using iPassport.Application.Models;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface ICompanyTypeService
    {
        Task<ResponseApi> GetAll();
    }
}
