using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class PriorityGroupRepository : Repository<PriorityGroup>, IPriorityGroupRepository
    {
        public PriorityGroupRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<PriorityGroup>> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _DbSet.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<IList<PriorityGroup>> FindByListName(List<string> listName)
            => await _DbSet.Where(m => listName.Contains(m.Name.ToUpper())).OrderBy(p => p.Name).ToListAsync();
    }
}
