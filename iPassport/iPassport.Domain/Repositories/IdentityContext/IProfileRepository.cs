using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IProfileRepository : IIdentityBaseRepository<Profile>
    {
        Task<IList<Profile>> GetAll(AccessControlDTO accessControl);
    }
}
