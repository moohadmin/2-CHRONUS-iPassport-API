using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;

namespace iPassport.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateBasic(Users user, UserDetails userDetails);
        string GenerateByEmail(Users user, UserDetails userDetails, string role);
    }
}