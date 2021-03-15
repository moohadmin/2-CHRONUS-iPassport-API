using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IGenderRepository : IIdentityBaseRepository<Gender>
    {
        Task<PagedData<Gender>> FindByNameParts(GetByNamePartsPagedFilter filter);
    }
}