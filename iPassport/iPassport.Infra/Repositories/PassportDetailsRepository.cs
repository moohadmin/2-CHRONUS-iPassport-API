using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class PassportDetailsRepository : Repository<PassportDetails>, IPassportDetailsRepository
    {
        public PassportDetailsRepository(iPassportContext context) : base(context) { }

    }
}
