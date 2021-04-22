using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Domain.Utils;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class ProfileRepository : IdentityBaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(PassportIdentityContext context) : base(context) { }

        private IQueryable<Profile> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
                query = query.Where(x => x.Key == EProfileKey.healthUnit.ToString());

            return query;
        }

        public async Task<IList<Profile>> GetAll(AccessControlDTO accessControl) =>
            await AccessControllBaseQuery(accessControl).ToListAsync();
    }
}
