using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserDetailsRepository : IRepository<UserDetails>
    {
        Task<UserDetails> GetByUserId(Guid id);
        Task<UserDetails> GetLoadedUserById(Guid id);
        Task<IList<ImportedUserDto>> GetImportedUserById(Guid[] ids);
        Task<UserDetails> GetWithHealtUnityById(Guid id);
        Task<Guid[]> GetVaccinatedUsersWithHealtUnityById(Guid id);
        Task<UserDetails> GetByPassportId(Guid id);
    }
}
