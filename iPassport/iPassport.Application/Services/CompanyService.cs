using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iPassport.Application.Extensions;

namespace iPassport.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        private readonly ICompanyTypeRepository _companyTypeRepository;
        private readonly ICompanySegmentRepository _companySegmentRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IHttpContextAccessor _accessor;

        public CompanyService(
            ICompanyRepository companyRepository,
            IStringLocalizer<Resource> localizer,
            IMapper mapper,
            ICityRepository cityRepository,
            ICompanyTypeRepository companyTypeRepository,
            IStateRepository stateRepository,
            IAddressRepository addressRepository,
            ICompanySegmentRepository companySegmentRepository,
            IHttpContextAccessor accessor)
        {
            _companyRepository = companyRepository;
            _localizer = localizer;
            _mapper = mapper;
            _cityRepository = cityRepository;
            _companyTypeRepository = companyTypeRepository;
            _stateRepository = stateRepository;
            _addressRepository = addressRepository;
            _companySegmentRepository = companySegmentRepository;
            _accessor = accessor;
        }

        public async Task<ResponseApi> Add(CompanyCreateDto dto)
        {
            await ValidateToSave(dto, dto.Address.CityId);

            var company = Company.Create(dto);
            if (!dto.IsActive.Value)
                company.Deactivate(_accessor.GetCurrentUserId());

            var result = await _companyRepository.InsertAsync(company);
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            var responseViewModel = _mapper.Map<CompanyCreateResponseViewModel>(company);
            responseViewModel.CanAssociate = await HasBranchCompanyToAssociate(company.Id);

            return new ResponseApi(true, _localizer["CompanyCreated"], responseViewModel);
        }

        public async Task<ResponseApi> Edit(CompanyEditDto dto)
        {
            var company = await _companyRepository.GetLoadedCompanyById(dto.Id);
            if (company == null)
                throw new BusinessException(_localizer["CompanyNotFound"]);

            await ValidateToSave(dto, dto.Address.CityId, true);

            company.ChangeCompany(dto);

            if (!dto.IsActive.Value)
                company.Deactivate(_accessor.GetCurrentUserId());

            var result = await _companyRepository.Update(company);
            if (!result)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["CompanyEdited"], company.Id);
        }

        public async Task<PagedResponseApi> FindByNameParts(GetCompaniesPagedFilter filter)
        {
            var res = await _companyRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<CompanyViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Companies"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetById(Guid id)
        {
            var res = await _companyRepository.GetLoadedCompanyById(id);

            var result = _mapper.Map<CompanyViewModel>(res);

            return new ResponseApi(true, _localizer["Companies"], result);
        }

        public async Task<ResponseApi> GetAllTypes()
        {
            var companyTypes = await _companyTypeRepository.FindAll();
            var companyTypeViewModels = _mapper.Map<IList<CompanyTypeViewModel>>(companyTypes);

            return new ResponseApi(true, _localizer["CompanyTypes"], companyTypeViewModels);
        }

        public async Task<PagedResponseApi> GetSegmetsByTypeId(Guid typeId, PageFilter filter)
        {
            var res = await _companySegmentRepository.GetPagedByTypeId(typeId, filter);
            
            var result = _mapper.Map<IList<CompanySegmentViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["CompanySegments"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<ResponseApi> GetHeadquartersCompanies(GetHeadquarterCompanyFilter filter)
        {
            IList<HeadquarterCompanyViewModel> res = null;

            var companyType = await _companyTypeRepository.Find(filter.CompanyTypeId);
            var companySegment = await _companySegmentRepository.Find(filter.SegmentId);

            if (companyType != null && companySegment != null)
            {
                if (filter.LocalityId == null && companyType.Identifyer == (int)ECompanyType.Government)
                    throw new BusinessException(string.Format(_localizer["RequiredField"], _localizer["Locality"]));

                if (companyType.Identifyer == (int)ECompanyType.Private && (string.IsNullOrWhiteSpace(filter.Cnpj) || filter.Cnpj.Length != 8 || !Regex.IsMatch(filter.Cnpj, "^[0-9]+$")))
                    throw new BusinessException(_localizer["CnpjRequiredForPrivateCompany"]);

                if (companyType.Identifyer == (int)ECompanyType.Private)
                    res = _mapper.Map<IList<HeadquarterCompanyViewModel>>(await _companyRepository.GetPrivateHeadquarters(filter.Cnpj, companySegment.Identifyer));

                else if (companyType.Identifyer == (int)ECompanyType.Government && companySegment.Identifyer == (int)ECompanySegmentType.Municipal)
                {
                    var state = await _stateRepository.Find(filter.LocalityId.Value);
                    res = _mapper.Map<IList<HeadquarterCompanyViewModel>>(await _companyRepository.GetPublicMunicipalHeadquarters(state.Id, state.CountryId));
                }

                else if (companyType.Identifyer == (int)ECompanyType.Government && companySegment.Identifyer == (int)ECompanySegmentType.State)
                    res = _mapper.Map<IList<HeadquarterCompanyViewModel>>(await _companyRepository.GetPublicStateHeadquarters(filter.LocalityId.Value));
            }

            return new ResponseApi(true, _localizer["Companies"], res);
        }

        public async Task<PagedResponseApi> GetSubsidiariesCandidatesPaged(Guid parentId, PageFilter filter)
        {
            var company = await _companyRepository.GetLoadedCompanyById(parentId);
            ValidateParentCompanyToAssociate(company);

            PagedData<Company> pagedCandidates = await GetSubsidiariesCandidatesToGovernmentPaged(company, filter);

            var responseViewModel = _mapper.Map<CompanySubsidiaryCandidateResponseViewModel>(company);
            var result = _mapper.Map<IList<CompanySubsidiaryCandidateViewModel>>(pagedCandidates.Data);
            responseViewModel.Candidates = result;

            return new PagedResponseApi(true, _localizer["SubsidiariesCandidatesCompanies"], pagedCandidates.PageNumber, pagedCandidates.PageSize, pagedCandidates.TotalPages, pagedCandidates.TotalRecords, responseViewModel);
        }

        #region Private
        private async Task<bool> ValidateToSave(CompanyAbstractDto dto, Guid cityId, bool isEdit = false)
        {
            if (dto.IsActive == null)
                throw new BusinessException(string.Format(_localizer["RequiredField"], _localizer["IsActive"]));

            if(!isEdit && await _companyRepository.CnpjAlreadyRegistered(dto.Cnpj))
                throw new BusinessException(string.Format(_localizer["DataAlreadyRegistered"], _localizer["Cnpj"]));

            await ValidateAddress(cityId);
            await ValidateSegment(dto, cityId, isEdit);

            return true;
        }

        private async Task<bool> ValidateAddress(Guid cityId)
        {
            if (await _cityRepository.Find(cityId) == null)
                throw new BusinessException(_localizer["CityNotFound"]);

            return true;
        }

        private async Task<bool> ValidateSegment(CompanyAbstractDto dto, Guid cityId, bool isEdit = false)
        {
            var segment = await _companySegmentRepository.GetLoaded(dto.SegmentId);
            if (segment == null)
                throw new BusinessException(_localizer["SegmentNotFound"]);

            await ValidatePrivateSegment(dto, segment);
            await ValidateGovernmentSegment(dto, segment, cityId, isEdit);

            return true;
        }

        private async Task<bool> ValidatePrivateSegment(CompanyAbstractDto dto, CompanySegment segment)
        {
            if (segment.IsPrivateType())
            {
                if (!dto.IsHeadquarters.HasValue)
                    throw new BusinessException(string.Format(_localizer["RequiredField"], _localizer["IsHeadquarters"]));

                if (!dto.IsHeadquarters.Value)
                {
                    if (!dto.ParentId.HasValue)
                        throw new BusinessException(string.Format(_localizer["RequiredField"], _localizer["ParentId"]));

                    var headquarter = await _companyRepository.GetLoadedCompanyById(dto.ParentId.Value);

                    if (headquarter == null || headquarter.DeactivationDate.HasValue ||!headquarter.IsPrivateHeadquarters() || headquarter.Segment.Identifyer != segment.Identifyer)
                        throw new BusinessException(_localizer["HeadquarterNotFoundOrNotValid"]);

                    if (!CnpjUtils.Valid(dto.Cnpj) || !headquarter.BranchCompanyCnpjIsValid(dto.Cnpj))
                        throw new BusinessException(string.Format(_localizer["BranchCnpjNotValid"], headquarter.Name));
                }
                else if (dto.ParentId.HasValue)
                    throw new BusinessException(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]));

            }
            return true;
        }

        private async Task<bool> ValidateGovernmentSegment(CompanyAbstractDto dto, CompanySegment segment, Guid cityId, bool isEdit)
        {
            if (segment.IsGovernmentType())
            {
                if (dto.IsHeadquarters.HasValue)
                    throw new BusinessException(string.Format(_localizer["FieldMustBeNull"], _localizer["IsHeadquarters"]));

                var city = await _cityRepository.FindLoadedById(cityId);

                if (segment.IsFederal())
                {
                    if (dto.ParentId.HasValue)
                        throw new BusinessException(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]));

                    if (!isEdit && await _companyRepository.HasSameSegmentAndLocaleGovernmentCompany(city.State.CountryId, ECompanySegmentType.Federal))
                        throw new BusinessException(string.Format(_localizer["CompanyAlreadyRegisteredToSegmentAndLocal"],_localizer["Country"]));
                }
                else
                {
                    IList<Company> canBeParentCompanies = new List<Company>();
                    if (segment.IsMunicipal())
                    {
                        if (!isEdit && await _companyRepository.HasSameSegmentAndLocaleGovernmentCompany(city.Id, ECompanySegmentType.Municipal))
                            throw new BusinessException(string.Format(_localizer["CompanyAlreadyRegisteredToSegmentAndLocal"], _localizer["City"]));

                        canBeParentCompanies = await _companyRepository.GetPublicMunicipalHeadquarters(city.StateId, city.State.CountryId);
                    }
                    else if(segment.IsState())
                    {
                        if (!isEdit && await _companyRepository.HasSameSegmentAndLocaleGovernmentCompany(city.StateId, ECompanySegmentType.State))
                            throw new BusinessException(string.Format(_localizer["CompanyAlreadyRegisteredToSegmentAndLocal"], _localizer["State"]));

                        canBeParentCompanies = await _companyRepository.GetPublicStateHeadquarters(city.State.CountryId);
                    }

                    if (canBeParentCompanies.Any(x => !x.DeactivationDate.HasValue))
                    {
                        if (!dto.ParentId.HasValue)
                            throw new BusinessException(string.Format(_localizer["RequiredField"], _localizer["ParentId"]));

                        if (canBeParentCompanies.FirstOrDefault(x => x.Id == dto.ParentId.Value) == null)
                            throw new BusinessException(_localizer["HeadquarterNotFoundOrNotValid"]);
                    }
                    else if (dto.ParentId.HasValue)
                        throw new BusinessException(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]));
                }
            }

            return true;
        }

        private async Task<bool> HasBranchCompanyToAssociate(Guid companyId)
        {
            var company = await _companyRepository.GetLoadedCompanyById(companyId);
            if(company != null && company.IsActive())
            {
                if (company.IsFederalGovernment())
                    return await _companyRepository.HasSubsidiariesCandidatesToFederalGovernment(company.Address.City.State.CountryId);
                if (company.IsStateGovernment())
                    return await _companyRepository.HasSubsidiariesCandidatesToStateGovernment(company.Address.City.StateId);
            }

            return false;
        }
        private void ValidateParentCompanyToAssociate(Company company)
        {
            if (company == null
                || !company.IsActive()
                || company.Segment == null
                || (!company.IsFederalGovernment() && !company.IsStateGovernment())
                || (company.Address == null))
                throw new BusinessException(_localizer["HeadquarterNotFoundOrNotValid"]);
        }

        private async Task<PagedData<Company>> GetSubsidiariesCandidatesToGovernmentPaged(Company company, PageFilter filter)
        {
            if (company.IsFederalGovernment())
                return await _companyRepository.GetSubsidiariesCandidatesToFederalGovernmentPaged(company.Address.City.State.CountryId, filter);
            else
                return await _companyRepository.GetSubsidiariesCandidatesToStateGovernmentPaged(company.Address.City.StateId, filter);
        }

        #endregion
    }
}

