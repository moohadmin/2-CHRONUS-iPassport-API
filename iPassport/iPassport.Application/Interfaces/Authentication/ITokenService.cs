using iPassport.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateBasic(Users user, bool hasPlan);
        Task<string> GenerateByEmail(Users user);
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}