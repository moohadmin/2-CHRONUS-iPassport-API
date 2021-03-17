using iPassport.Domain.Dtos;
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
    public class UserVaccineRepository : Repository<UserVaccine>, IUserVaccineRepository
    {
        public UserVaccineRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<UserVaccineDetailsDto>> GetPagedUserVaccinesByUserId(GetByIdPagedFilter pageFilter)
        {
            var q = await _DbSet
               .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
               .Include(v => v.UserDetails).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
               .Where(v => v.UserDetails.Id == pageFilter.Id)
               .OrderBy(x => x.Vaccine.Name)
               .ToListAsync();

            return GeneratePaggedVaccineDetails(q, pageFilter);
        }

        public async Task<PagedData<UserVaccineDetailsDto>> GetPagedUserVaccinesByPassportId(GetByIdPagedFilter pageFilter)
        {
            var q = await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.UserDetails).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
                .Where(v => v.UserDetails.Passport.ListPassportDetails.Any(x => x.Id == pageFilter.Id))
                .OrderBy(x => x.Vaccine.Name)
                .ToListAsync();

            return GeneratePaggedVaccineDetails(q, pageFilter);
        }

        public async Task<int> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            return await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.Vaccine).ThenInclude(v => v.Diseases)
                .Where(v => (v.VaccinationDate >= filter.StartTime && v.VaccinationDate <= filter.EndTime)
                    && (filter.ManufacturerId == null || v.Vaccine.ManufacturerId == filter.ManufacturerId)
                    && (filter.DiseaseId == null || v.Vaccine.Diseases.Any(d => d.Id == filter.DiseaseId))
                    && (filter.DosageCount == 0 ? v.Vaccine.RequiredDoses == 1 : v.Dose == filter.DosageCount && v.Vaccine.RequiredDoses > 1))
                .CountAsync();
        }

        public async Task<IList<VaccineIndicatorDto>> GetVaccinatedCountByManufacturer(GetVaccinatedCountFilter filter)
        {
            var query = await _DbSet
                .Include(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                .Include(v => v.Vaccine).ThenInclude(v => v.Diseases)
                .Where(v => (v.VaccinationDate >= filter.StartTime && v.VaccinationDate <= filter.EndTime)
                    && (filter.ManufacturerId == null || v.Vaccine.ManufacturerId == filter.ManufacturerId)
                    && (filter.DiseaseId == null || v.Vaccine.Diseases.Any(d => d.Id == filter.DiseaseId))
                    && (filter.DosageCount == 0 ? v.Vaccine.RequiredDoses == 1 : v.Dose == filter.DosageCount && v.Vaccine.RequiredDoses > 1))
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
                    UniqueDose = v.FirstOrDefault().Vaccine.RequiredDoses == 1,
                    Count = v.Count()

                }).OrderBy(v => v.Disease).ToList();

            return result;
        }

        private PagedData<UserVaccineDetailsDto> GeneratePaggedVaccineDetails(IList<UserVaccine> userVaccines, PageFilter pageFilter)
        {
            var q = userVaccines.GroupBy(v => new { v.VaccineId, v.Vaccine.Name })
            .Select(v => new UserVaccineDetailsDto()
            {
                UserId = v.FirstOrDefault().UserId,
                VaccineId = v.Key.VaccineId,
                VaccineName = v.Key.Name,
                RequiredDoses = v.FirstOrDefault().Vaccine.RequiredDoses,
                ImmunizationTime = v.FirstOrDefault().Vaccine.ImmunizationTimeInDays,
                Doses = v.Select(x => new VaccineDoseDto()
                {
                    Dose = x.Dose,
                    VaccinationDate = x.VaccinationDate,
                    ExpirationDate = x.VaccinationDate.AddMonths(x.Vaccine.ExpirationTimeInMonths)
                })
            });

            (int take, int skip) = CalcPageOffset(pageFilter);

            var data = q.Take(take).Skip(skip).ToList();
            
            int totalPages = 0;
            if (q.Count() < pageFilter.PageSize)
            {
                totalPages = 1;
            }
            else
            {
                totalPages = q.Count() / pageFilter.PageSize;
                totalPages = q.Count() % pageFilter.PageSize > 0 ? totalPages + 1 : totalPages;
            }

            return new PagedData<UserVaccineDetailsDto>()
            {
                PageNumber = pageFilter.PageNumber,
                PageSize = pageFilter.PageSize,
                TotalPages = totalPages,
                TotalRecords = data.Count,
                Data = data
            };
        }
    }
}
