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
        private readonly IAddressRepository _addressRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HealthUnitService(IHealthUnitRepository healthUnitRepository,
                                IStringLocalizer<Resource> localizer,
                                IMapper mapper,
                                IAddressRepository addressRepository,
                                ICityRepository cityRepository,
                                IUnitOfWork unitOfWork)
        {
            _healthUnitRepository = healthUnitRepository;
            _localizer = localizer;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi> Add(HealthUnitCreateDto dto)
        {
            var city = await _cityRepository.Find(dto.Address.CityId);

            if (city == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            try
            {
                _unitOfWork.BeginTransactionIdentity();
                _unitOfWork.BeginTransactionPassport();

                var address = new Address().Create(dto.Address);
                await _addressRepository.InsertAsync(address);

                // Ine must be informed when exists cnpj in database
                if (string.IsNullOrWhiteSpace(dto.Ine) && await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null)
                    throw new BusinessException(_localizer["IneRequired"]);

                dto.Address.Id = address.Id;
                var healthUnit = new HealthUnit().Create(dto);

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
            var address = await _addressRepository.Find(dto.Address.Id);
            if (address == null)
                throw new BusinessException(_localizer["AddressNotFound"]);

            var unit = await _healthUnitRepository.Find(dto.Id);
            if (unit == null)
                throw new BusinessException(_localizer["HealthUnitNotFound"]);

            if (string.IsNullOrWhiteSpace(dto.Ine) && await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null)
                throw new BusinessException(_localizer["IneRequired"]);

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

            return new ResponseApi(true, _localizer["HealthUnitUpdated"], address.Id);
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
            var res = await _healthUnitRepository.Find(id);
            var result = _mapper.Map<HealthUnitViewModel>(res);

            if (res.AddressId.HasValue)
            {
                var resultAddress = await _addressRepository.FindFullAddress(res.AddressId.Value);
                result.Address = _mapper.Map<AddressViewModel>(resultAddress);
            }

            return new ResponseApi(true, _localizer["HealthUnits"], result);
        }
    }
}
