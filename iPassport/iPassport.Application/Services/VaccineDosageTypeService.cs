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
    public class VaccineDosageTypeService : IVaccineDosageTypeService
    {
        private readonly IVaccineDosageTypeRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public VaccineDosageTypeService(IVaccineDosageTypeRepository repository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _repository = repository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<ResponseApi> GetAll() =>
            new ResponseApi(true, _localizer["VaccineDosageTypes"], _mapper.Map<IList<VaccineDosageTypeViewModel>>(await _repository.FindAll()));
    }
}
