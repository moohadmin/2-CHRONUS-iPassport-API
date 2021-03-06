using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class StateRepository : IdentityBaseRepository<State>, IStateRepository
    {
        public StateRepository(PassportIdentityContext context) : base(context) { }

    }
}
