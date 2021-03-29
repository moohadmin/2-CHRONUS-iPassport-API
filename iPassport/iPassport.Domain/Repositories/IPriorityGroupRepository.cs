using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IPriorityGroupRepository : IRepository<PriorityGroup>
    {
        Task<PagedData<PriorityGroup>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<IList<PriorityGroup>> FindByListName(List<string> listName);
    }
}