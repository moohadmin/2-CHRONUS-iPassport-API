using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICityRepository : IIdentityBaseRepository<City>
    {
        Task<PagedData<City>> FindByStateAndNameParts(GetByIdAndNamePartsPagedFilter filter);
    }
}