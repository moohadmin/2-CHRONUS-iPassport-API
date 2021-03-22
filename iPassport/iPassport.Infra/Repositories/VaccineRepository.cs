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
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<Vaccine>> GetByManufacturerId(GetByIdAndNamePartsPagedFilter filter)
        {
            var query = _DbSet
                .Include(x => x.Manufacturer)
                .Where(m => m.ManufacturerId == filter.Id
                    && (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())))
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<IList<Vaccine>> GetByVaccineAndManufacturerNames(List<string> filter)
            => await _DbSet.Include(v => v.Manufacturer)
                .Where(m => filter.Any(f => f == m.Name.ToUpper() + m.Manufacturer.Name.ToUpper()))
                .ToListAsync();

    }
}
