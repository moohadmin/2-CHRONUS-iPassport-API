using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IStateRepository : IIdentityBaseRepository<State>
    {
        Task<PagedData<State>> GetByCountryId(GetByIdPagedFilter filter, AccessControlDTO accessControl);
    }
}
