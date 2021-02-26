using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class IndicatorService : IIndicatorService
    {
        private readonly IUserVaccineRepository _userVaccineRepository;

        public IndicatorService(IUserVaccineRepository userVaccineRepository)
        {
            _userVaccineRepository = userVaccineRepository;
        }

        public async Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            var res = await _userVaccineRepository.GetVaccinatedCount(filter);

            return new ResponseApi(true, "Total de vacinados", res);
        }
    }
}
