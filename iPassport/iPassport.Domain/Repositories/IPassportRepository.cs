using iPassport.Domain.Entities;

using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{

    public interface IPassportRepository : IRepository<Passport>
    {
        Task<Passport> FindByUser(System.Guid userId);

        Task<Passport> FindByPassportDetailsValid(System.Guid passportDetailsId);
    }
}
