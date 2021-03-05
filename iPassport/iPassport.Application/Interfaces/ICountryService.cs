using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface ICountryService
    {
        //Task<ResponseApi> Add(CountryCreateDto dto);
        Task<ResponseApi> GetAll();
        Task<ResponseApi> FindById(System.Guid id);

    }
}
