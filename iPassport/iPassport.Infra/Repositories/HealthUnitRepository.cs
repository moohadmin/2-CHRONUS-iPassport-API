using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class HealthUnitRepository : Repository<HealthUnit>, IHealthUnitRepository
    {
        public HealthUnitRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<HealthUnit>> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _DbSet.Include(x => x.Type)
                               .Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower()))
                               .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
