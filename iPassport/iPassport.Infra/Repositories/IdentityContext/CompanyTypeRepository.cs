using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CompanyTypeRepository : IdentityBaseRepository<CompanyType>, ICompanyTypeRepository
    {
        public CompanyTypeRepository(PassportIdentityContext context) : base(context) { }

        private IQueryable<CompanyType> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString() || accessControl.Profile == EProfileKey.business.ToString())
            {
                query = query.Where(c => accessControl.FilterIds == null  || !accessControl.FilterIds.Any() || accessControl.FilterIds.Contains(c.Id));
            }

            return query;
        }

        public async Task<IList<CompanyType>> GetAllTypes(AccessControlDTO accessControl) 
            => await AccessControllBaseQuery(accessControl).ToListAsync();
    }
}
