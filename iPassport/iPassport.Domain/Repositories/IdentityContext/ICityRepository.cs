using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICityRepository : IIdentityBaseRepository<City>
    {
        Task<PagedData<City>> FindByStateAndNameParts(GetByIdAndNamePartsPagedFilter filter, AccessControlDTO accessControl);
        Task<IList<City>> FindByCityStateAndCountryNames(List<string> filter);

        Task<City> FindLoadedById(System.Guid id);
    }
}