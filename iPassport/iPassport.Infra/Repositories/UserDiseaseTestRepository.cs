using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDiseaseTestRepository : Repository<UserDiseaseTest>, IUserDiseaseTestRepository
    {
        public UserDiseaseTestRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<UserDiseaseTest>> GetPaggedUserDiseaseTestsByPassportId(GetByIdPagedFilter pageFilter)
        {
            var q = _DbSet
                .Include(v => v.Disease)
                .Include(v => v.User).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
                .Where(v => v.User.Passport.ListPassportDetails.Any(x => x.Id == pageFilter.Id))
                .OrderByDescending(x => x.TestDate);

            return await Paginate(q, pageFilter);
        }

        public async Task<PagedData<UserDiseaseTest>> GetPagedUserDiseaseTestsByUserId(GetByIdPagedFilter pageFilter)
        {
            var q = _DbSet
               .Include(v => v.Disease)
               .Include(v => v.User).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
               .Where(v => v.User.Id == pageFilter.Id)
               .OrderByDescending(x => x.TestDate);

            return await Paginate(q, pageFilter);
        }
    }
}
