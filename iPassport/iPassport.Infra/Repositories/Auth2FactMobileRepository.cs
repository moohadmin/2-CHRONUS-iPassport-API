using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class Auth2FactMobileRepository : Repository<Auth2FactMobile>, IAuth2FactMobileRepository
    {
        public Auth2FactMobileRepository(iPassportContext context) : base(context) { }
    }
}
