using iPassport.Domain.Entities;

namespace iPassport.Application.Interfaces
{
    public interface ITokenService
    {
         string GenerateBasic(User user);
    }
}