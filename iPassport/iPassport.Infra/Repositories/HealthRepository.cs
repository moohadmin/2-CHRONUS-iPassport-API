using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class HealthRepository : Repository<Health>, IHealthRepository
    {
        public HealthRepository(iPassportContext context) : base(context) { }
    }
}
