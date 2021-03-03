using iPassport.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserDetailsRepository : IRepository<UserDetails>
    {
        Task<UserDetails> GetByUserId(Guid id);

    }
}
