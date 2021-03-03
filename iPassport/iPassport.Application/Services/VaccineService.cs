using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IUserVaccineRepository _userVaccineRepository;
        private readonly IStringLocalizer<Resource> _localizer;

        public VaccineService(IUserVaccineRepository userVaccineRepository, IStringLocalizer<Resource> localizer)
        {
            _userVaccineRepository = userVaccineRepository;
            _localizer = localizer;
        }

        public async Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            var res = await _userVaccineRepository.GetVaccinatedCount(filter);

            return new ResponseApi(true, _localizer["VaccinatedCount"], res);
        }
    }
}
