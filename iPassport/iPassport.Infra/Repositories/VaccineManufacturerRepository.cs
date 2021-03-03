using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class VaccineManufacturerRepository : Repository<VaccineManufacturer>, IVaccineManufacturerRepository
    {
        public VaccineManufacturerRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<VaccineManufacturer>> GetByNameInitals(GetByNameInitalsPagedFilter filter)
        {
            var query = _DbSet.Where(m => m.Name.ToLower().StartsWith(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
