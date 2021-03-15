using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserVaccineRepository : IRepository<UserVaccine>
    {
        Task<PagedData<UserVaccineDetailsDto>> GetPagedUserVaccinesByPassportId(GetByIdPagedFilter filter);

        Task<int> GetVaccinatedCount(GetVaccinatedCountFilter filter);
        Task<IList<VaccineIndicatorDto>> GetVaccinatedCountByManufacturer(GetVaccinatedCountFilter filter);
        Task<PagedData<UserVaccineDetailsDto>> GetPagedUserVaccinesByUserId(GetByIdPagedFilter pageFilter);
    }
}
