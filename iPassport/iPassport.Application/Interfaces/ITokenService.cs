using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;

namespace iPassport.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateBasic(Users user, bool hasPlan);
        string GenerateByEmail(Users user, string role);
    }
}