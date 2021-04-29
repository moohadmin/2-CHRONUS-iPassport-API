using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class UserTypeRepository : IdentityBaseRepository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(PassportIdentityContext context) : base(context) { }
                
    }
}
