using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Utils;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDiseaseTestRepository : Repository<UserDiseaseTest>, IUserDiseaseTestRepository
    {
        public UserDiseaseTestRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<UserDiseaseTest>> GetPaggedUserDiseaseTestsByPassportId(GetByIdPagedFilter pageFilter)
        {
            var q = GetActiveTests().Where(v => v.User.Passport.ListPassportDetails.Any(x => x.Id == pageFilter.Id));

            return await Paginate(q, pageFilter);
        }

        public async Task<PagedData<UserDiseaseTest>> GetPagedUserDiseaseTestsByUserId(GetByIdPagedFilter pageFilter)
        {
            var q = GetActiveTests().Where(v => v.User.Id == pageFilter.Id);

            return await Paginate(q, pageFilter);
        }

        private IQueryable<UserDiseaseTest> GetActiveTests() =>
            _DbSet.Include(v => v.User).ThenInclude(d => d.Passport).ThenInclude(p => p.ListPassportDetails)
                .Where(x => (x.TestDate - DateTime.UtcNow).TotalHours <= Constants.DISEATE_TEST_VALIDATE_IN_HOURS)
                .OrderByDescending(x => x.TestDate);
    }
}
