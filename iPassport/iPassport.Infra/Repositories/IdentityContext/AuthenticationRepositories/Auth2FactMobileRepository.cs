using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class Auth2FactMobileRepository : Repository<Auth2FactMobile>, IAuth2FactMobileRepository
    {
        public Auth2FactMobileRepository(iPassportContext context) : base(context) { }

        public async Task<Auth2FactMobile> FindByUserAndPin(Guid id, string pin)
        {
            var res = await _DbSet.Where(x => x.UserId == id && x.Pin == pin && x.IsValid).FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<Auth2FactMobile>> FindByUser(Guid id)
        {
            var res = await _DbSet.Where(x => x.UserId == id).ToListAsync();

            return res;
        }
    }
}
