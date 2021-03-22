using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class HealthUnitTypeRepository : Repository<HealthUnitType>, IHealthUnitTypeRepository
    {
        public HealthUnitTypeRepository(iPassportContext context) : base(context) { }
    }
}
