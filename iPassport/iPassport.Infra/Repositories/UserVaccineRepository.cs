using iPassport.Domain.Dtos;
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
    public class UserVaccineRepository : Repository<UserVaccine>, IUserVaccineRepository
    {
        public UserVaccineRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<UserVaccine>> GetPagedUserVaccines(Guid userId, PageFilter pageFilter)
        {
            var query = _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Where(v => v.UserId == userId)
                .OrderBy(v => v.VaccinationDate);

            return await Paginate(query, pageFilter);
        }

        public async Task<int> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            return await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.Vaccine).ThenInclude(v => v.Diseases)
                .Where(v => (v.VaccinationDate >= filter.StartTime && v.VaccinationDate <= filter.EndTime)
                    && (filter.ManufacturerId == null || v.Vaccine.ManufacturerId == filter.ManufacturerId)
                    && (v.Vaccine.Diseases.Any(d => d.Id == filter.DiseaseId))
                    && (filter.DosageCount == 0 ? v.Vaccine.UniqueDose == true : v.Dose == filter.DosageCount && v.Vaccine.UniqueDose == false))
                .CountAsync();
        }

        public async Task<IList<VaccineIndicatorDto>> GetVaccinatedCountByManufacturer(GetVaccinatedCountFilter filter)
        {
            var query = await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.Vaccine).ThenInclude(v => v.Diseases)
                .Where(v => (v.VaccinationDate >= filter.StartTime && v.VaccinationDate <= filter.EndTime)
                    && (filter.ManufacturerId == null || v.Vaccine.ManufacturerId == filter.ManufacturerId)
                    && (v.Vaccine.Diseases.Any(d => d.Id == filter.DiseaseId))
                    && (filter.DosageCount == 0 ? v.Vaccine.UniqueDose == true : v.Dose == filter.DosageCount && v.Vaccine.UniqueDose == false))
                .ToListAsync();

            var result = query.GroupBy(v => new { v.Dose, v.VaccineId, v.Vaccine.ManufacturerId })
                .Select(v => new VaccineIndicatorDto()
                {
                    VaccnineId = v.Key.VaccineId,
                    VaccineName = v.FirstOrDefault().Vaccine.Name,
                    Disease = v.FirstOrDefault().Vaccine.Diseases.FirstOrDefault(d => d.Id == filter.DiseaseId).Name,
                    ManufacturerId = v.Key.ManufacturerId,
                    ManufacturerName = v.FirstOrDefault().Vaccine.Manufacturer.Name,
                    Dose = v.Key.Dose,
                    UniqueDose = v.FirstOrDefault().Vaccine.UniqueDose,
                    Count = v.Count()

                }).OrderBy(v => v.Disease).ToList();

            return result;
        }
    }
}
