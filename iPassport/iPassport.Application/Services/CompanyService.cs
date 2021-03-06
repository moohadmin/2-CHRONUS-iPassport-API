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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;        
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        //public async Task<ResponseApi> Add(CountryCreateDto dto)
        //{
        //    var country = new Country().Create(dto);

        //    var result = await _countryRepository.InsertAsync(country);
        //    if(!result)
        //        throw new BusinessException(_localizer["OperationNotPerformed"]);

        //    return new ResponseApi(true, _localizer["CountryCreated"], country.Id);
        //}
        //public async Task<ResponseApi> FindById(System.Guid id)
        //{
        //    var country = await _countryRepository.Find(id);

        //    if(country == null)
        //        throw new BusinessException(_localizer["CountryNotFound"]);

        //    var countryViewModel = _mapper.Map<CountryViewModel>(country);

        //    return new ResponseApi(true, _localizer["Country"], countryViewModel);
        //}

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _companyRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<CompanyViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Companies"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
