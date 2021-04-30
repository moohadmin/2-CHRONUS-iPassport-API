using iPassport.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateBasic(Users user, bool hasPlan, string IdentifyUserType);
        Task<string> GenerateByEmail(Users user, string CompanyId, string CityId, string StateId, string CountryId, string HealthUnityId);
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}