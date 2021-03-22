using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IGenderRepository : IIdentityBaseRepository<Gender>
    {
        Task<PagedData<Gender>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<IList<Gender>> FindByListNames(List<string> names);
    }
}