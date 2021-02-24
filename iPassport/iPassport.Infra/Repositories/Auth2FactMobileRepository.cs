using System;
using System.Linq;
using System.Threading.Tasks;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace iPassport.Infra.Repositories
{
    public class Auth2FactMobileRepository : Repository<Auth2FactMobile>, IAuth2FactMobileRepository
    {
        public Auth2FactMobileRepository(iPassportContext context) : base(context) { }

        public async Task<Auth2FactMobile> FindByUserAndPin(Guid id, string pin) =>
            await _DbSet.Where(x => x.UserId == id && x.Pin == pin).FirstOrDefaultAsync();
    }
}
