using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class ImportedFileService : IImportedFileService
    {
        private readonly IMapper _mapper;
        private readonly IImportedFileRepository _repository;
        private readonly IImportedFileDetailsRepository _importedFileDetailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<Resource> _localizer;

        public ImportedFileService(IMapper mapper, IImportedFileRepository repository, 
            IStringLocalizer<Resource> localizer, IUserRepository userRepository, IImportedFileDetailsRepository importedFileDetailsRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
            _userRepository = userRepository;
            _importedFileDetailsRepository = importedFileDetailsRepository;
        }

        public async Task<PagedResponseApi> FindByPeriod(GetImportedFileFilter filter)
        {
            if (filter.StartTime.Date.AddYears(1) < filter.EndTime.Date)
                throw new BusinessException(_localizer["InvalidPeriod"]);
            
            var res = await _repository.FindByPeriod(filter);
            var result = _mapper.Map<IList<ImportedFileViewModel>>(res.Data);

            foreach (var item in result)
            {
                var user = await _userRepository.GetById(item.UserId);
                item.CompleteName = user.FullName;
            }
            return new PagedResponseApi(true, _localizer["ImportedFiles"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetImportedFileDetails(Guid fileId)
        {
            var res = await _importedFileDetailsRepository.GetByFileId(fileId);
            var result = _mapper.Map<IList<ImportedFileDetailsViewModel>>(res);

            return new ResponseApi(true, _localizer["ImportedFiles"], result);
        }
    }
}
