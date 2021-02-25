using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserVaccineRepository : Repository<UserVaccine>, IUserVaccineRepository
    {
        public UserVaccineRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<UserVaccine>> GetPagedUserVaccines(Guid userId, PageFilter pageFilter)
        {
            var query = _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Where(v => v.UserId == userId);

            return await Paginate(query, pageFilter);
        }

        public async Task<int> GetVaccinatedCount(VaccinatedCountFilter filter)
        {
            var query = await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.Vaccine).ThenInclude(v => v.Diseases)
                .Where(v => (v.Vaccine != null && v.Vaccine.Diseases != null)
                    && (v.VaccinationDate >= filter.StartTime && v.VaccinationDate <= filter.EndTime)
                    && (filter.ManufacturerId == null || v.Vaccine.ManufacturerId == filter.ManufacturerId)
                    && (filter.DiseaseId == null || v.Vaccine.Diseases.Any(d => d.Id == filter.DiseaseId))
                    && (filter.DosageCount == null || v.Dose == filter.DosageCount))
                .CountAsync();

            return query;
        }
    }
}
