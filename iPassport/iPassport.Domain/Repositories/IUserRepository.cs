using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindWithDoc(string doc);
    }
}
