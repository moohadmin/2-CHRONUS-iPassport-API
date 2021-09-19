using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<PagedData<Disease>> GetByNameInitals(GetByNamePartsPagedFilter filter);
        Task<IList<Disease>> GetByIdList(IEnumerable<Guid> diseases);
    }
}
