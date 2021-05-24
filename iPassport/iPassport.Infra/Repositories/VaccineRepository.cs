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

        public async Task<PagedData<Vaccine>> GetPagged(GetPagedVaccinesFilter filter)
        {
            var query = _DbSet
                .Include(x => x.Manufacturer)
                .Include(x => x.Diseases)
                .Where(v => (filter.ManufacuterId == null || v.ManufacturerId == filter.ManufacuterId.Value)
                            && (filter.DiseaseId == null || v.Diseases.Any(x => x.Id == filter.DiseaseId.Value))
                            && (filter.DosageTypeId == null || v.DosageTypeId == filter.DosageTypeId.Value)
                            && (string.IsNullOrWhiteSpace(filter.Initials) || v.Name.ToLower().Contains(filter.Initials.ToLower())))
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<IList<Vaccine>> GetByVaccineAndManufacturerNames(List<string> filter)
            => await _DbSet.Include(v => v.Manufacturer)
                .Where(m => filter.Any(f => f == m.Name.ToUpper() + m.Manufacturer.Name.ToUpper()))
                .ToListAsync();
    }
}
