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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public ProfileService(IProfileRepository profileRepository, IMapper mapper, IStringLocalizer<Resource> localizer)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<ResponseApi> GetAll()
        { 
            var res = _mapper.Map<IList<ProfileViewModel>>(await _profileRepository.FindAll());

            return new ResponseApi(true, _localizer["Profiles"], res);
        }
    }
}
