using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDetailsRepository : Repository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(iPassportContext context) : base(context) { }

        public async Task<UserDetails> GetByUserId(Guid id) =>
            await _DbSet.Include(u => u.Plan).Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<UserDetails> GetLoadedUserById(Guid id) =>
            await _DbSet.Include(u => u.UserVaccines).ThenInclude(v => v.Vaccine)
                        .Include(u => u.UserVaccines).ThenInclude(v => v.HealthUnit).ThenInclude(u => u.Type)
                        .Include(x => x.Plan)
                        .Include(x => x.UserDiseaseTests)
                        .Include(x => x.PPriorityGroup)
                        .Include(x => x.Passport).ThenInclude(x => x.ListPassportDetails)
                        .Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
