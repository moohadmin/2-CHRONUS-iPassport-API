using iPassport.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateBasic(Users user);
        Task<string> GenerateByEmail(Users user, string role);
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}