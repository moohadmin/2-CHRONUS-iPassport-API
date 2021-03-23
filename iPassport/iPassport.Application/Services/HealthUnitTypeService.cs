using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class HealthUnitTypeService : IHealthUnitTypeService
    {
        private readonly IMapper _mapper;
        private readonly IHealthUnitTypeRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;

        public HealthUnitTypeService(IMapper mapper, IHealthUnitTypeRepository repository, IStringLocalizer<Resource> localizer)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<ResponseApi> GetAll()
        {
            var res = await _repository.FindAll();

            var result = _mapper.Map<IList<HealthUnitTypeViewModel>>(res);

            return new ResponseApi(true, _localizer["HealthUnitTypes"], result);
        }
    }
}
