using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class UserTypeRepository : IdentityBaseRepository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(PassportIdentityContext context) : base(context) { }

        public async Task<UserType> GetByIdentifier(int identifyer) => await _DbSet.FirstOrDefaultAsync(x => x.Identifyer == identifyer);
    }
}
