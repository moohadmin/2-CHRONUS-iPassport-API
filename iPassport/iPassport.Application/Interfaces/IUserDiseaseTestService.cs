using iPassport.Application.Models.Pagination;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IUserDiseaseTestService
    {
        Task<PagedResponseApi> GetCurrentUserDiseaseTest(PageFilter pageFilter);
        Task<PagedResponseApi> GetUserDiseaseTest(GetByIdPagedFilter pageFilter);
    }
}
