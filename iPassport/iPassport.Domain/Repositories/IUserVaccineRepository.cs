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
        Task<PagedData<UserVaccine>> GetPagedUserVaccines(Guid userId, PageFilter pageFilter);

        Task<IList<VaccineIndicatorDto>> GetVaccinatedCount(GetVaccinatedCountFilter filter);
    }
}
