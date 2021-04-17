using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
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

        public async Task<PagedData<HealthUnit>> GetPagedHealthUnits(GetHealthUnitPagedFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl, filter.Locations);

            query = query.Include(x => x.Type)
                               .Where(m => (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower()))
                                        && (string.IsNullOrWhiteSpace(filter.Cnpj) || m.Cnpj.Contains(filter.Cnpj))
                                        && (string.IsNullOrWhiteSpace(filter.Ine) || m.Ine.Contains(filter.Ine))
                                        && (filter.CompanyId == null|| filter.CompanyId == m.CompanyId)
                                        && (filter.TypeId == null|| filter.TypeId == m.TypeId))
                               .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<HealthUnit> GetByCnpj(string cnpj) =>
            await _DbSet.FirstOrDefaultAsync(x => x.Cnpj == cnpj);

        public async Task<HealthUnit> GetByIne(string ine) =>
            await _DbSet.FirstOrDefaultAsync(x => x.Ine == ine);

        public async Task<int> GetNexUniqueCodeValue()
        {
            var currentCode = await _DbSet.Where(x => x.UniqueCode != null).MaxAsync(x => x.UniqueCode);

            return currentCode.HasValue ? currentCode.Value + 1 : 1;
        }

        public async Task<IList<HealthUnit>> FindByCnpjAndIne(List<string> listCnpj, List<string> listIne)
            => await _DbSet.Where(m => listCnpj.Any(l => l == m.Cnpj)).Union(_DbSet.Where(m => listIne.Any(l => l == m.Ine))).ToListAsync();

        public async Task<HealthUnit> GetLoadedById(Guid id) =>
            await _DbSet.Include(x => x.Type).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IList<HealthUnit>> FindByCnpjIneAndCode(List<string> listCnpj, List<string> listIne, List<int?> listCode)
            => await _DbSet.Where(m => listCnpj.Any(l => l == m.Cnpj)).Union(_DbSet.Where(m => listIne.Any(l => l == m.Ine))).Union(_DbSet.Where(m => listCode.Any(l => l == m.UniqueCode))).ToListAsync();

        private IQueryable<HealthUnit> AccessControllBaseQuery(AccessControlDTO accessControl, IList<Guid> locations)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.business.ToString())
            {
                query = query.Where(x => x.CompanyId == accessControl.CompanyId);
            }
            else if(accessControl.Profile == EProfileKey.government.ToString() || accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                query = query.Where(x => x.AddressId != null && locations.Contains(x.AddressId.Value));
            }

            return query;
        }
    }
}
