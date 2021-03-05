using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;        
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> Add(CountryCreateDto dto)
        {
            throw new System.NotImplementedException();
        }
        public async Task<ResponseApi> FindById(System.Guid id)
        {
            var country = await _countryRepository.Find(id);

            if(country == null)
                throw new BusinessException(_localizer["CountryNotFound"]);

            var countryViewModel = _mapper.Map<CountryViewModel>(country);

            return new ResponseApi(true, "País", countryViewModel);
        }

        public async Task<PagedResponseApi> GetByNameInitials(GetByNameInitialsPagedFilter filter)
        {
            var res = await _countryRepository.GetByNameInitials(filter);

            var result = _mapper.Map<IList<CountryViewModel>>(res.Data);

            return new PagedResponseApi(true, "Paises", res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
