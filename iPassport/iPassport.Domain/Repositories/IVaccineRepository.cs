using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        Task<PagedData<Vaccine>> GetPagedUserVaccines(Guid UserId, PageFilter filter);
    }
}
