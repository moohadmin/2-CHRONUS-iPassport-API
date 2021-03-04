using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class DiseaseRepository : Repository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<Disease>> GetByNameInitals(GetByNameInitalsFilter filter)
        {
            var query = _DbSet.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().StartsWith(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
