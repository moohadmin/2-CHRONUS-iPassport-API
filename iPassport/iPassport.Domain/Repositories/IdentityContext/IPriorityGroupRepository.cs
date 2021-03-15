using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IPriorityGroupRepository : IIdentityBaseRepository<PriorityGroup>
    {
        Task<PagedData<PriorityGroup>> FindByNameParts(GetByNamePartsPagedFilter filter);
    }
}