using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class HealthUnitService : IHealthUnitService
    {
        private readonly IHealthUnitRepository _healthUnitRepository;
        private readonly IHealthUnitTypeRepository _healthUnitTypeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HealthUnitService(IHealthUnitRepository healthUnitRepository,
                                IHealthUnitTypeRepository healthUnitTypeRepository,
                                IStringLocalizer<Resource> localizer,
                                IMapper mapper,
                                IAddressRepository addressRepository,
                                ICityRepository cityRepository,
                                ICompanyRepository companyRepository,
                                IUnitOfWork unitOfWork)
        {
            _healthUnitRepository = healthUnitRepository;
            _healthUnitTypeRepository = healthUnitTypeRepository;
            _localizer = localizer;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi> Add(HealthUnitCreateDto dto)
        {
            if (await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            if (await _cityRepository.Find(dto.Address.CityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var type = await _healthUnitTypeRepository.Find(dto.TypeId.Value);
            if (type == null)
                throw new BusinessException(_localizer["HealthUnitTypeNotFound"]);

            var hasCnpj = await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null;

            // Ine must be informed when exists cnpj in database and Health Unit Type is Public 
            if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && hasCnpj)
                throw new BusinessException(_localizer["IneRequired"]);

            if (type.Identifyer == (int)EHealthUnitType.Private && string.IsNullOrWhiteSpace(dto.Cnpj))
                throw new BusinessException(string.Format(_localizer["RequiredField"], "CNPJ"));

            if (type.Identifyer == (int)EHealthUnitType.Private && hasCnpj)
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNPJ"));

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                var address = new Address().Create(dto.Address);
                await _addressRepository.InsertAsync(address);

                dto.Address.Id = address.Id;
                var healthUnit = new HealthUnit().Create(dto);

                // When Health Unit type is public, and Ine and Cnpj is null the unique code must be declared
                if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && string.IsNullOrWhiteSpace(dto.Cnpj))
                    healthUnit.AddUniqueCode(await _healthUnitRepository.GetNexUniqueCodeValue());

                await _healthUnitRepository.InsertAsync(healthUnit);

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();

                return new ResponseApi(true, _localizer["HealthUnitCreated"], healthUnit.Id);

            }
            catch (Exception)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                throw;
            }
        }

        public async Task<ResponseApi> Edit(HealthUnitEditDto dto)
        {
            if (await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);
            
            var address = await _addressRepository.Find(dto.Address.Id);
            if (address == null)
                throw new BusinessException(_localizer["AddressNotFound"]);

            var unit = await _healthUnitRepository.Find(dto.Id);
            if (unit == null)
                throw new BusinessException(_localizer["HealthUnitNotFound"]);

            var type = await _healthUnitTypeRepository.Find(dto.TypeId.Value);
            if (type == null)
                throw new BusinessException(_localizer["HealthUnitTypeNotFound"]);

            var hasCnpj = await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null;

            // Ine must be informed when exists cnpj in database and Health Unit Type is Public 
            if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && hasCnpj)
                throw new BusinessException(_localizer["IneRequired"]);

            if (type.Identifyer == (int)EHealthUnitType.Private && string.IsNullOrWhiteSpace(dto.Cnpj))
                throw new BusinessException(string.Format(_localizer["RequiredField"], "CNPJ"));

            if(unit.Cnpj != dto.Cnpj && type.Identifyer == (int)EHealthUnitType.Private && hasCnpj)
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNPJ"));

            try
            {
                address.ChangeAddress(dto.Address);
                unit.ChangeHealthUnit(dto);

                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                if (!await _healthUnitRepository.Update(unit))
                    throw new BusinessException(_localizer["OperationNotPerformed"]);

                if (!await _addressRepository.Update(address))
                    throw new BusinessException(_localizer["OperationNotPerformed"]);

                _unitOfWork.CommitIdentity();
                _unitOfWork.CommitPassport();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackIdentity();
                _unitOfWork.RollbackPassport();

                throw;
            }

            return new ResponseApi(true, _localizer["HealthUnitUpdated"], unit.Id);
        }

        public async Task<PagedResponseApi> FindByNameParts(GetHealthUnitPagedFilter filter)
        {
            var res = await _healthUnitRepository.GetPagedHealthUnits(filter);

            var result = _mapper.Map<IList<HealthUnitViewModel>>(res.Data);

            foreach (var item in result.Where(x => x.AddressId.HasValue))
            {
                var address = await _addressRepository.FindFullAddress(item.AddressId.Value);
                item.Address = _mapper.Map<AddressViewModel>(address);
            }

            return new PagedResponseApi(true, _localizer["HealthUnits"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetById(Guid id)
        {
            var res = await _healthUnitRepository.GetLoadedById(id);
            var result = _mapper.Map<HealthUnitViewModel>(res);

            if (res.AddressId.HasValue)
            {
                var resultAddress = await _addressRepository.FindFullAddress(res.AddressId.Value);
                result.Address = _mapper.Map<AddressViewModel>(resultAddress);
            }

            if (res.CompanyId.HasValue)
            {
                var resultCompany = await _companyRepository.Find(res.CompanyId.Value);
                result.Company = _mapper.Map<CompanyViewModel>(resultCompany);
            }

            return new ResponseApi(true, _localizer["HealthUnits"], result);
        }
    }
}
