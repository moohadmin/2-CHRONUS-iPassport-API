using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IUserVaccineRepository _userVaccineRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        
        public VaccineService(IVaccineRepository vaccineRepository, IUserVaccineRepository userVaccineRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _vaccineRepository = vaccineRepository;
            _userVaccineRepository = userVaccineRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            var res = await _userVaccineRepository.GetVaccinatedCount(filter);

            return new ResponseApi(true, _localizer["VaccinatedCount"], res);
        }

        public async Task<ResponseApi> GetByManufacturerId(GetByIdPagedFilter filter)
        {
            var res = await _vaccineRepository.GetByManufacturerId(filter);
            var data = _mapper.Map<IList<VaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Vaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }
    }
}
