using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class ProfileRepository : IdentityBaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(PassportIdentityContext context) : base(context) { }

        
    }
}
