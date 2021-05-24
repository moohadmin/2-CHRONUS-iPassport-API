using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IVaccineService
    {
        Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter);
        Task<ResponseApi> GetByManufacturerId(GetPagedVaccinesFilter filter);
        Task<ResponseApi> GetPagged(GetPagedVaccinesFilter filter);
        Task<ResponseApi> Add(VaccineDto dto);
    }
}
