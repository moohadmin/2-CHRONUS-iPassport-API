using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class CompanyTypeService : ICompanyTypeService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyTypeRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;

        public CompanyTypeService(IMapper mapper, ICompanyTypeRepository repository, IStringLocalizer<Resource> localizer)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<ResponseApi> GetAll()
        {
            var companyTypes = await _repository.FindAll();
            var companyTypeViewModels = _mapper.Map<IList<CompanyTypeViewModel>>(companyTypes);

            return new ResponseApi(true, _localizer["CompanyTypes"], companyTypeViewModels);
        }
    }
}
