using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(iPassportContext context) : base(context) { }

    public async Task<PagedData<Vaccine>> GetByManufacturerId(GetByIdPagedFilter filter)
    {
            var query = _DbSet
                .Include(x => x.Manufacturer)
                .Where(m => m.ManufacturerId == filter.Id)
                .OrderBy(m => m.Name);

        return await Paginate(query, filter);
    }
}
}
