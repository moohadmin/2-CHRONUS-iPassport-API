using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDetailsRepository : Repository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(iPassportContext context) : base(context) { }

        public async Task<UserDetails> FindWithUser(Guid id) =>
            await _DbSet.Where(x => x.UserId == id).FirstOrDefaultAsync();

        public async Task<UserDetails> FindByDocument(int documentType, string document)
        {
            if(documentType == 1)
                return await _DbSet.Where(x => x.CNS == document).FirstOrDefaultAsync();
            
            if(documentType == 2)
                return await _DbSet.Where(x => x.CPF == document).FirstOrDefaultAsync();
            
            if(documentType == 3)
                return await _DbSet.Where(x => x.PassportDoc == document).FirstOrDefaultAsync();
            
            if(documentType == 4)
                return await _DbSet.Where(x => x.RG == document).FirstOrDefaultAsync();

            return null;

            //await _DbSet.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        //public async Task<UserDetails> FindByPhone(string phone) =>
        //    await _DbSet.Where(x => x.)

    }
}
