using iPassport.Application.Models;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IVaccineService
    {
        Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter);
        Task<ResponseApi> GetByManufacturerId(GetByIdAndNamePartsPagedFilter filter);
    }
}
