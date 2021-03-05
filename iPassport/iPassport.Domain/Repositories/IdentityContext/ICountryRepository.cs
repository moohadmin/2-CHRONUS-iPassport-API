using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICountryRepository : IIdentityBaseRepository<Country>
    {
        Task<PagedData<Country>> GetByNameInitials(GetByNameInitialsPagedFilter filter);
    }
}
