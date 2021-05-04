using iPassport.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateBasic(Users user, bool hasPlan, string identifyUserType);
        Task<string> GenerateByEmail(Users user, string companyId, string cityId, string stateId, string countryId, string healthUnityId, string isFirstLoginText, string identifyUserType);
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}