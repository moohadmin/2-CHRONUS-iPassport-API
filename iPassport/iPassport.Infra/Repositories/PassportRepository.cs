using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class PassportRepository : Repository<Passport>, IPassportRepository
    {
        public PassportRepository(iPassportContext context) : base(context) { }

        public async Task<Passport> FindByUser(Guid id) =>
            await _DbSet.Where(x => x.UserDetails.UserId == id)
                    .Include(x => x.ListPassportDetails).FirstOrDefaultAsync();
    }
}
