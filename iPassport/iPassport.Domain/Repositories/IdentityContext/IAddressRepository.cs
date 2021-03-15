using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IAddressRepository : IIdentityBaseRepository<Address>
    {
        Task<Address> FindFullAddress(System.Guid id);
    }
}
