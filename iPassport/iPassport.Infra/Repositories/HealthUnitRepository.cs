using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class HealthUnitRepository : Repository<HealthUnit>, IHealthUnitRepository
    {
        public HealthUnitRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<HealthUnit>> GetPagedHealthUnits(GetHealthUnitPagedFilter filter)
        {
            var query = _DbSet.Include(x => x.Type)
                               .Where(m => (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower()))
                                        && (string.IsNullOrWhiteSpace(filter.Cnpj) || m.Cnpj.Contains(filter.Cnpj))
                                        && (string.IsNullOrWhiteSpace(filter.Ine) || m.Ine.Contains(filter.Ine)))
                               .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<HealthUnit> GetByCnpj(string cnpj) =>
            await _DbSet.FirstOrDefaultAsync(x => x.Cnpj == cnpj);

        public async Task<int> GetNexUniqueCodeValue()
        {
            var currentCode = await _DbSet.Where(x => x.UniqueCode != null).MaxAsync(x => x.UniqueCode);

            return currentCode.HasValue ? currentCode.Value + 1 : 1;
        }

        public async Task<IList<HealthUnit>> FindByCnpjAndIne(List<string> listCnpj, List<string> listIne)
            => await _DbSet.Where(m => listCnpj.Any(l => l == m.Cnpj)).Union(_DbSet.Where(m => listIne.Any(l => l == m.Ine))).ToListAsync();

        public async Task<HealthUnit> GetLoadedById(Guid id) =>
            await _DbSet.Include(x => x.Type).FirstOrDefaultAsync(x => x.Id == id);
    }
}
