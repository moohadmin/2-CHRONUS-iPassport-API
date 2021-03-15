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

        public async Task<PagedData<UserDiseaseTest>> GetPaggedUserDiseaseByPassportId(GetByIdPagedFilter pageFilter)
        {
            var q = _DbSet
                .Include(v => v.Disease)
                .Include(v => v.User).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
                .Where(v => v.User.Passport.ListPassportDetails.Any(x => x.Id == pageFilter.Id));

            return await Paginate(q, pageFilter);
        }

        public async Task<PagedData<UserDiseaseTest>> GetPagedUserDiseaseByUserId(GetByIdPagedFilter pageFilter)
        {
            var q = _DbSet
               .Include(v => v.Disease)
               .Include(v => v.User).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
               .Where(v => v.User.Id == pageFilter.Id);

            return await Paginate(q, pageFilter);
        }
    }
}
