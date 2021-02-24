using System;
using System.Threading.Tasks;
using iPassport.Domain.Entities;

namespace iPassport.Domain.Repositories
{
    public interface IAuth2FactMobileRepository : IRepository<Auth2FactMobile>
    {
        Task<Auth2FactMobile> FindByUserAndPin(Guid id, string pin);
    }
}
