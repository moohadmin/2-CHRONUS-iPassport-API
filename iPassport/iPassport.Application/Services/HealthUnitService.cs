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
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;

        public HealthUnitService(IHealthUnitRepository healthUnitRepository,
                                IHealthUnitTypeRepository healthUnitTypeRepository,
                                IStringLocalizer<Resource> localizer,
                                IMapper mapper,
                                IAddressRepository addressRepository,
                                ICityRepository cityRepository,
                                ICompanyRepository companyRepository,
                                IUnitOfWork unitOfWork,
                                IHttpContextAccessor accessor)
        {
            _healthUnitRepository = healthUnitRepository;
            _healthUnitTypeRepository = healthUnitTypeRepository;
            _localizer = localizer;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }

        public async Task<ResponseApi> Add(HealthUnitCreateDto dto)
        {
            if (await _companyRepository.Find(dto.CompanyId.Value) == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            var healthUnityCity = await _cityRepository.FindLoadedById(dto.Address.CityId);
            if (healthUnityCity == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var type = await _healthUnitTypeRepository.Find(dto.TypeId.Value);
            if (type == null)
                throw new BusinessException(_localizer["HealthUnitTypeNotFound"]);

            var hasCnpj = dto.Cnpj != null && await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null;

            // Ine must be informed when exists cnpj in database and Health Unit Type is Public 
            if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && hasCnpj)
                throw new BusinessException(_localizer["IneRequired"]);

            if (type.Identifyer == (int)EHealthUnitType.Private && string.IsNullOrWhiteSpace(dto.Cnpj))
                throw new BusinessException(string.Format(_localizer["RequiredField"], "CNPJ"));

            if (type.Identifyer == (int)EHealthUnitType.Private && hasCnpj)
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNPJ"));

            if (dto.Ine != null && await _healthUnitRepository.GetByIne(dto.Ine) != null)
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "INE"));

            ValidateToSaveHealthUnityPermission(healthUnityCity, type);

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
            var company = await _companyRepository.Find(dto.CompanyId.Value);
            if (company == null)
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

            var hasCnpj = dto.Cnpj != null && await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null;

            // Ine must be informed when exists cnpj in database and Health Unit Type is Public 
            if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && hasCnpj)
                throw new BusinessException(_localizer["IneRequired"]);

            if (type.Identifyer == (int)EHealthUnitType.Private && string.IsNullOrWhiteSpace(dto.Cnpj))
                throw new BusinessException(string.Format(_localizer["RequiredField"], "CNPJ"));

            if(unit.Cnpj != dto.Cnpj && type.Identifyer == (int)EHealthUnitType.Private && hasCnpj)
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], "CNPJ"));

            // When Health Unit type is public, and Ine and Cnpj is null the unique code must be declared
            if (type.Identifyer == (int)EHealthUnitType.Public && string.IsNullOrWhiteSpace(dto.Ine) && string.IsNullOrWhiteSpace(dto.Cnpj) && !unit.UniqueCode.HasValue)
                unit.AddUniqueCode(await _healthUnitRepository.GetNexUniqueCodeValue());

            var healthUnityCity = await _cityRepository.FindLoadedById(address.CityId);
            ValidateToSaveHealthUnityPermission(healthUnityCity, type);

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

            return new ResponseApi(true, _localizer["HealthUnitUpdated"], await GetUpdatedViewModel(unit, address.Id, company));
        }

        private async Task<HealthUnitViewModel> GetUpdatedViewModel(HealthUnit updatedUnit, Guid addressId, Company company)
        {
            updatedUnit.Type = await _healthUnitTypeRepository.Find(updatedUnit.TypeId);
            var result = _mapper.Map<HealthUnitViewModel>(updatedUnit);
            result.Address = _mapper.Map<AddressViewModel>(await _addressRepository.FindFullAddress(addressId));
            result.Company = _mapper.Map<CompanyViewModel>(company);
            return result;
        }

        public async Task<PagedResponseApi> FindByNameParts(GetHealthUnitPagedFilter filter)
        {
            filter.Locations = await FilterLocation(_accessor.GetAccessControlDTO());
            
            var res = await _healthUnitRepository.GetPagedHealthUnits(filter, _accessor.GetAccessControlDTO());


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

        #region Private Methods
        private async Task<IList<Guid>> FilterLocation(AccessControlDTO accessControl)
        {
            IList<Guid> locations = new List<Guid>();

            if (accessControl.Profile == EProfileKey.government.ToString() || accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    locations = await _addressRepository.GetCityAddresses(accessControl.CityId.Value);

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    locations = await _addressRepository.GetStateAddresses(accessControl.StateId.Value);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    locations = await _addressRepository.GetCountryAddresses(accessControl.CountryId.Value);
            }
            return locations;
        }

        private void ValidateToSaveHealthUnityPermission(City healthUnityCity, HealthUnitType healthUnitType)
        {
            var acessControll = _accessor.GetAccessControlDTO();

            if (acessControll.Profile == EProfileKey.government.ToString())
            {
                if (healthUnitType.Identifyer != (int)EHealthUnitType.Public)
                    throw new BusinessException(_localizer["LoggedInUserCanOnlyRegisterHealthUnitiesWithPublicType"]);

                if (((acessControll.CityId.HasValue && acessControll.CityId.Value != healthUnityCity.Id) ||
                    (acessControll.StateId.HasValue && acessControll.StateId.Value != healthUnityCity?.StateId) ||
                    (acessControll.CountryId.HasValue && acessControll.CountryId.Value != healthUnityCity?.State.CountryId)))
                    throw new BusinessException(_localizer["LoggedInUserCanOnlyRegisterHealthUnitiesWithSameLocationAsHis"]);
            }
        }

        #endregion
    }
}
