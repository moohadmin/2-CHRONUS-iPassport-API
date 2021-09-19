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
    public class VaccinePeriodTypeService : IVaccinePeriodTypeService
    {
        private readonly IVaccinePeriodTypeRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public VaccinePeriodTypeService(IVaccinePeriodTypeRepository repository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _repository = repository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> GetAll() =>
            new ResponseApi(true, _localizer["VaccinePeriodTypes"], _mapper.Map<IList<VaccinePeriodTypeViewModel>>(await _repository.FindAll()));
    }
}
