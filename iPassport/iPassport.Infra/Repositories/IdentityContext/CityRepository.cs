using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CityRepository : IdentityBaseRepository<City>, ICityRepository
    {
        public CityRepository(PassportIdentityContext context) : base(context) { }

    }
}
