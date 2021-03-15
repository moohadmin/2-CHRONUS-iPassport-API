using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class HealthUnitService : IHealthUnitService
    {
        private readonly IHealthUnitRepository  _healthUnitRepository;
        private readonly IAddressRepository  _addressRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public HealthUnitService(IHealthUnitRepository healthUnitRepository, IStringLocalizer<Resource> localizer, IMapper mapper, IAddressRepository addressRepository)
        {
            _healthUnitRepository = healthUnitRepository;
            _localizer = localizer;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _healthUnitRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<HealthUnitViewModel>>(res.Data);

            foreach (var item in result.Where(x => x.AddressId.HasValue))
            {
                var address = await _addressRepository.FindFullAddress(item.AddressId.Value);
                item.Address =  _mapper.Map<AddressViewModel>(address);
            }

            return new PagedResponseApi(true, _localizer["HealthUnits"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
