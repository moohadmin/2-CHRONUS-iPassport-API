using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineDosageTypeRepository _vaccineDosageTypeRepository;
        private readonly IVaccinePeriodTypeRepository _vaccinePeriodTypeRepository;
        private readonly IUserVaccineRepository _userVaccineRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public VaccineService(IVaccineRepository vaccineRepository,
            IUserVaccineRepository userVaccineRepository,
            IVaccinePeriodTypeRepository vaccinePeriodTypeRepository,
            IVaccineDosageTypeRepository vaccineDosageTypeRepository,
            IStringLocalizer<Resource> localizer,
            IMapper mapper)
        {
            _vaccineRepository = vaccineRepository;
            _userVaccineRepository = userVaccineRepository;
            _vaccinePeriodTypeRepository = vaccinePeriodTypeRepository;
            _vaccineDosageTypeRepository = vaccineDosageTypeRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            var res = await _userVaccineRepository.GetVaccinatedCount(filter);

            return new ResponseApi(true, _localizer["VaccinatedCount"], res);
        }

        public async Task<ResponseApi> GetByManufacturerId(GetByIdAndNamePartsPagedFilter filter)
        {
            var res = await _vaccineRepository.GetByManufacturerId(filter);
            var data = _mapper.Map<IList<VaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Vaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }

        public async Task<ResponseApi> Add(VaccineDto dto)
        {
            await GetvaccinePeriodType(dto);
            await GetVaccineDosageType(dto);

            var vaccine = Vaccine.Create(dto);

            if (!await _vaccineRepository.InsertAsync(vaccine))
                throw new BusinessException(_localizer["OperationNotPeformed"]);

            return new ResponseApi(true, _localizer["VaccineCreated"], vaccine.Id);
        }

        private async Task GetVaccineDosageType(VaccineDto dto)
        {
            var dosageType = await _vaccineDosageTypeRepository.GetByIdentifyer(dto.DosageType);
            dto.DosageTypeId = dosageType.Id;
        }

        private async Task GetvaccinePeriodType(VaccineDto dto)
        {
            if (dto.GeneralGroupVaccine != null)
            {
                var periodType = await _vaccinePeriodTypeRepository.GetByIdentifyer(dto.GeneralGroupVaccine.PeriodType);
                dto.GeneralGroupVaccine.PeriodTypeId = periodType.Id;
            }
            else if (dto.AgeGroupVaccines != null && dto.AgeGroupVaccines.Any())
            {
                var periodType = await _vaccinePeriodTypeRepository.GetByIdentifyer(dto.AgeGroupVaccines.FirstOrDefault().PeriodType);

                foreach (var ageGroup in dto.AgeGroupVaccines)
                    ageGroup.PeriodTypeId = periodType.Id;
            }
        }
    }
}
