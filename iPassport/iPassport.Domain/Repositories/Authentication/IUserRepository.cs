using iPassport.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserRepository
    {
        Task<Users> FindByPhone(string phone);
    }
}
