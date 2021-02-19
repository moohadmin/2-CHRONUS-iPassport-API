using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> BasicLogin(string username, string password);
        Task<User> LoginWithEmail(string email, string password);
    }
}
