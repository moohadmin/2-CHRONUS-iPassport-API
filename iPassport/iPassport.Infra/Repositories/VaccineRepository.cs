using iPassport.Application.Exceptions;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(iPassportContext context) : base(context) { }

        public async Task<bool> AssociateDiseases(Vaccine vaccine, IList<Disease> diseases)
        {
            try
            {
                vaccine.SetUpdateDate();

                vaccine.AddDiseases(diseases);
                
                _context.Entry(vaccine).State = EntityState.Modified;

                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.GetType() == (typeof(PostgresException)))
                {
                    var key = ((PostgresException)ex.InnerException).ConstraintName.Split('_').Last();

                    throw new UniqueKeyException(key, ex);
                }

                throw new PersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<PagedData<Vaccine>> GetPagged(GetPagedVaccinesFilter filter)
        {
            var query = _DbSet
                .Include(x => x.Manufacturer)
                .Include(x => x.Diseases)
                .Include(x => x.AgeGroupVaccines)
                .Include(x => x.GeneralGroupVaccine)
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
