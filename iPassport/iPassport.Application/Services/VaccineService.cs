using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
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
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public VaccineService(IVaccineRepository vaccineRepository,
            IUserVaccineRepository userVaccineRepository,
            IVaccinePeriodTypeRepository vaccinePeriodTypeRepository,
            IVaccineDosageTypeRepository vaccineDosageTypeRepository,
            IDiseaseRepository diseaseRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor accessor,
            IStringLocalizer<Resource> localizer,
            IMapper mapper)
        {
            _vaccineRepository = vaccineRepository;
            _userVaccineRepository = userVaccineRepository;
            _vaccinePeriodTypeRepository = vaccinePeriodTypeRepository;
            _vaccineDosageTypeRepository = vaccineDosageTypeRepository;
            _diseaseRepository = diseaseRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter)
        {
            var res = await _userVaccineRepository.GetVaccinatedCount(filter);

            return new ResponseApi(true, _localizer["VaccinatedCount"], res);
        }
        public async Task<ResponseApi> GetByManufacturerId(GetVaccineByManufacturerFilter filter)
        {
            var res = await _vaccineRepository.GetPagged(filter);
            var data = _mapper.Map<IList<VaccineGetByManufacturerViewModel>>(res.Data);
            data.ToList().ForEach(x => x.RequiredDoses = res.Data.Single(r => r.Id == x.Id).GetRequiredDoses(filter.Birthday));

            return new PagedResponseApi(true, _localizer["Vaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }

        public async Task<ResponseApi> GetPagged(GetPagedVaccinesFilter filter)
        {

            if (filter.DosageTypeId != null) {
                filter.VaccineDosageType = await _vaccineDosageTypeRepository.GetByIdentifyer((int)filter.DosageTypeId);
            }

            var res = await _vaccineRepository.GetPagged(filter);
            
            var data = _mapper.Map<List<VaccineViewModel>>(res.Data);
            
            data.ForEach(x => x.DiseaseNames = res.Data.FirstOrDefault(y => y.Id == x.Id).Diseases != null ? string.Join(", ", res.Data.FirstOrDefault(y => y.Id == x.Id).Diseases.Select(z => z.Name)) : null);

            return new PagedResponseApi(true, _localizer["Vaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }

        public async Task<ResponseApi> Add(VaccineDto dto)
        {
            try
            {
                await GetvaccinePeriodType(dto);
                await GetVaccineDosageType(dto);

                var vaccine = Vaccine.Create(dto);
                var diseases = await _diseaseRepository.GetByIdList(dto.Diseases);
                
                if (!dto.IsActive.GetValueOrDefault())
                    vaccine.Deactivate(_accessor.GetCurrentUserId());

                _unitOfWork.BeginTransactionPassport();

                if (!await _vaccineRepository.InsertAsync(vaccine))
                    throw new BusinessException(_localizer["OperationNotPeformed"]);

                if (!await _vaccineRepository.AssociateDiseases(vaccine, diseases))
                    throw new BusinessException(_localizer["OperationNotPeformed"]);

                _unitOfWork.CommitPassport();

                return new ResponseApi(true, _localizer["VaccineCreated"], vaccine.Id);
            }
            catch (Exception)
            {
                _unitOfWork.RollbackPassport();

                throw;
            }
        }

        private async Task GetVaccineDosageType(VaccineDto dto)
        {
            var dosageType = await _vaccineDosageTypeRepository.GetByIdentifyer((int)dto.DosageType);
            dto.DosageTypeId = dosageType.Id;
        }

        private async Task GetvaccinePeriodType(VaccineDto dto)
        {
            if (dto.GeneralGroupVaccine != null && dto.GeneralGroupVaccine.PeriodType != null)
            {
                var periodType = await _vaccinePeriodTypeRepository.GetByIdentifyer((int)dto.GeneralGroupVaccine.PeriodType);
                dto.GeneralGroupVaccine.PeriodTypeId = periodType.Id;
            }
            else if (dto.AgeGroupVaccines != null && dto.AgeGroupVaccines.All(x => x.PeriodType != null))
            {
                var periodType = await _vaccinePeriodTypeRepository.GetByIdentifyer((int)dto.AgeGroupVaccines.FirstOrDefault().PeriodType);

                foreach (var ageGroup in dto.AgeGroupVaccines)
                    ageGroup.PeriodTypeId = periodType.Id;
            }
        }
    }
}
