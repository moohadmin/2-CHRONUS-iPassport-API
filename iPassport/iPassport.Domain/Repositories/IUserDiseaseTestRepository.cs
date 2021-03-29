using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserDiseaseTestRepository : IRepository<UserDiseaseTest>
    {
        Task<PagedData<UserDiseaseTest>> GetPaggedUserDiseaseTestsByPassportId(GetByIdPagedFilter pageFilter);
        Task<PagedData<UserDiseaseTest>> GetPagedUserDiseaseTestsByUserId(GetByIdPagedFilter pageFilter);
    }
}
