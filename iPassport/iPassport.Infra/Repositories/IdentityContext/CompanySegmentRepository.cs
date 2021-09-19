using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CompanySegmentRepository : IdentityBaseRepository<CompanySegment>, ICompanySegmentRepository
    {
        public CompanySegmentRepository(PassportIdentityContext context) : base(context) { }

        private IQueryable<CompanySegment> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if  (accessControl.Profile == EProfileKey.business.ToString())            
                query = query.Where(c => accessControl.FilterIds == null || !accessControl.FilterIds.Any() || accessControl.FilterIds.Contains(c.Id));

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                //uses the location to distinguish the business segment associated with the logged in user and perform the filters
                int segmentIdentifyer = 0;
                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    segmentIdentifyer = (int)ECompanySegmentType.Federal;
                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    segmentIdentifyer = (int)ECompanySegmentType.State;
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    segmentIdentifyer = (int)ECompanySegmentType.Municipal;
                
                query = query.Where(x => x.Identifyer <= segmentIdentifyer);
            }
           return query;
        }

        public async Task<PagedData<CompanySegment>> GetPagedByTypeId(Guid id, PageFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl);

            query = query.Where(m => m.CompanyTypeId == id)
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<CompanySegment> GetLoaded(Guid id) =>
             await _DbSet.Include(x => x.CompanyType).Where(m => m.Id == id).FirstOrDefaultAsync();
                

    }
}
