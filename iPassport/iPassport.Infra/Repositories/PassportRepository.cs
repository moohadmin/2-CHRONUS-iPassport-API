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

        public async Task<Passport> FindByUser(Guid userId) =>
            await _DbSet.Where(x => x.UserDetails.UserId == userId)
                    .Include(x => x.ListPassportDetails).FirstOrDefaultAsync();

        public async Task<Passport> FindByPassportDetailsValid(Guid passportDetailsId)
        {

            var today = DateTime.UtcNow.Date;
            return await _DbSet.Where(x => x.ListPassportDetails.Any(z => z.Id == passportDetailsId && z.ExpirationDate.Date > today))
                        .Include(x => x.ListPassportDetails)
                        .Include(x => x.UserDetails).ThenInclude(y => y.UserVaccines).ThenInclude(z => z.Vaccine)                        
                        .FirstOrDefaultAsync();
        }

    }
}
