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
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        private readonly ICompanyTypeRepository _companyTypeRepository;

        public CompanyService(ICompanyRepository companyRepository, IStringLocalizer<Resource> localizer, IMapper mapper, ICityRepository cityRepository, ICompanyTypeRepository companyTypeRepository)
        {
            _companyRepository = companyRepository;
            _localizer = localizer;
            _mapper = mapper;
            _cityRepository = cityRepository;
            _companyTypeRepository = companyTypeRepository;
        }

        public async Task<ResponseApi> Add(CompanyCreateDto dto)
        {
            var city = await _cityRepository.Find(dto.AddressDto.CityId);
            if(city == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            var company = new Company().Create(dto);

            var result = await _companyRepository.InsertAsync(company);
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["CompanyCreated"], company.Id);
        }

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _companyRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<CompanyViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Companies"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetById(Guid id)
        {
            var res = await _companyRepository.GetLoadedCompanyById(id);

            var result = _mapper.Map<CompanyViewModel>(res);

            return new ResponseApi(true, _localizer["Companiies"], result);
        }

        public async Task<ResponseApi> GetAllTypes()
        {
            var companyTypes = await _companyTypeRepository.FindAll();
            var companyTypeViewModels = _mapper.Map<IList<CompanyTypeViewModel>>(companyTypes);

            return new ResponseApi(true, _localizer["CompanyTypes"], companyTypeViewModels);
        }
    }
}

