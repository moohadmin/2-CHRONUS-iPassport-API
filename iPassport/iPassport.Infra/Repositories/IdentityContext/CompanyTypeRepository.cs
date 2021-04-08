using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CompanyTypeRepository : IdentityBaseRepository<CompanyType>, ICompanyTypeRepository
    {
        public CompanyTypeRepository(PassportIdentityContext context) : base(context) { }

     
    }
}
