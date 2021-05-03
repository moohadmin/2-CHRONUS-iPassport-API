using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IUserTypeRepository : IIdentityBaseRepository<UserType>
    {
        Task<UserType> GetByIdentifier(int identifyer);
    }
}