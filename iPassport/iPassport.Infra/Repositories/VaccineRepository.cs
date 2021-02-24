using iPassport.Domain.Dtos;
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
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<Vaccine>> GetPagedUserVaccines(Guid userId, PageFilter pageFilter)
        {
            var query = _DbSet.Where(v => v.UserVaccines.Any(x => x.UserId == userId));
            
            return await Paginate(query, pageFilter);
        }
    }
}
