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
        private readonly IHealthUnitRepository  _healthUnitRepository;
        private readonly IAddressRepository  _addressRepository;
        private readonly ICityRepository  _cityRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public HealthUnitService(IHealthUnitRepository healthUnitRepository, IStringLocalizer<Resource> localizer, IMapper mapper, IAddressRepository addressRepository, ICityRepository cityRepository)
        {
            _healthUnitRepository = healthUnitRepository;
            _localizer = localizer;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ResponseApi> Add(HealthUnitCreateDto dto)
        {
            var city = await _cityRepository.Find(dto.Address.CityId);

            if (city == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var address = new Address().Create(dto.Address);
            if (!await _addressRepository.InsertAsync(address))
                throw new BusinessException(_localizer["OperationNotPerformed"]);
            
            // Ine must be informed when exists cnpj in database
            if(string.IsNullOrWhiteSpace(dto.Ine) && await _healthUnitRepository.GetByCnpj(dto.Cnpj) != null)
                throw new BusinessException(_localizer["IneRequired"]);

            var healthUnit = new HealthUnit().Create(dto);

            var result = await _healthUnitRepository.InsertAsync(healthUnit);
            
            if (!result)
            {
                await _addressRepository.Delete(address);   
                throw new BusinessException(_localizer["OperationNotPerformed"]);
            }

            return new ResponseApi(true, _localizer["HealthUnitCreated"], healthUnit.Id);
        }

        public async Task<PagedResponseApi> FindByNameParts(GetHealthUnitPagedFilter filter)
        {
            var res = await _healthUnitRepository.GetPagedHealthUnits(filter);

            var result = _mapper.Map<IList<HealthUnitViewModel>>(res.Data);

            foreach (var item in result.Where(x => x.AddressId.HasValue))
            {
                var address = await _addressRepository.FindFullAddress(item.AddressId.Value);
                item.Address =  _mapper.Map<AddressViewModel>(address);
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
