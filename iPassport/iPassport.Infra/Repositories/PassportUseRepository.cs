using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class PassportUseRepository : Repository<PassportUse>, IPassportUseRepository
    {
        public PassportUseRepository(iPassportContext context) : base(context) { }

    }
}
