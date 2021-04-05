using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class ProfileRepository : IdentityBaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(PassportIdentityContext context) : base(context) { }

        
    }
}
