using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDetailsRepository : Repository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(iPassportContext context) : base(context) { }

        public async Task<UserDetails> GetByUserId(Guid id) =>
            await _DbSet.Include(u => u.Plan).Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<UserDetails> GetLoadedUserById(Guid id) =>
            await _DbSet.Include(u => u.UserVaccines).ThenInclude(v => v.Vaccine).ThenInclude(v => v.Manufacturer)
                        .Include(u => u.UserVaccines).ThenInclude(v => v.HealthUnit).ThenInclude(u => u.Type)
                        .Include(x => x.Plan)
                        .Include(x => x.UserDiseaseTests)
                        .Include(x => x.PPriorityGroup)
                        .Include(x => x.Passport).ThenInclude(x => x.ListPassportDetails)
                        .Include(x => x.ImportedFile)
                        .Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<IList<ImportedUserDto>> GetImportedUserById(Guid[] ids)
        {
            return await _DbSet.Where(x => ids.Contains(x.Id))
                .Select(y => new ImportedUserDto() {
                            UserId =  y.Id,
                            WasImported = y.ImportedFileId != null
            }).ToListAsync();
        }

        public async Task<UserDetails> GetWithHealtUnityById(Guid id) =>
            await _DbSet.Include(x => x.HealthUnit).FirstOrDefaultAsync(x => x.Id == id);
    }
}
