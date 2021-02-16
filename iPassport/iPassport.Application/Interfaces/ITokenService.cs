using iPassport.Domain.Entities;

namespace iPassport.Application.Interfaces
{
    public interface ITokenService
    {
         string Generate(User user);
    }
}