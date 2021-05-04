using iPassport.Domain.Dtos;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class Company : Entity
    {
        public Company() { }
        public Company(Guid id) => Id = id;

        public Company(string name, string tradeName, string cnpj, AddressCreateDto addressDto, Guid? segmentId, bool? isHeadquarters,
            Guid? parentId, CompanyResponsibleDto responsible)
        {
            Id = Guid.NewGuid();
            Name = name;
            TradeName = tradeName;
            Cnpj = cnpj;
            Address = CreateCompanyAddress(addressDto);
            SegmentId = segmentId;
            IsHeadquarters = isHeadquarters;
            ParentId = parentId;
            if (responsible != null)
                Responsible = CreateResponsible(Id, responsible);
        }

        public string Name { get; private set; }
        public string TradeName { get; private set; }
        public string Cnpj { get; private set; }
        public Guid AddressId { get; private set; }
        public Guid? SegmentId { get; private set; }
        public bool? IsHeadquarters { get; private set; }
        public Guid? ParentId { get; private set; }
        public DateTime? DeactivationDate { get; set; }
        public Guid? DeactivationUserId { get; set; }

        public virtual Address Address { get; set; }
        public virtual CompanySegment Segment { get; set; }
        public virtual Company ParentCompany { get; set; }
        public virtual CompanyResponsible Responsible { get; set; }
        public virtual Users DeactivationUser { get; set; }
        public virtual IEnumerable<Company> Subsidiaries { get; set; }

        public static Company Create(CompanyCreateDto dto)
                => new Company(dto.Name, dto.TradeName, dto.Cnpj, dto.Address, dto.SegmentId, dto.IsHeadquarters, dto.ParentId, dto.Responsible);

        private Address CreateCompanyAddress(AddressCreateDto dto) => new Address().Create(dto);

        public void ChangeCompany(CompanyEditDto dto)
        {
            Address.ChangeAddress(dto.Address);
            Responsible?.ChangeResponsible(dto.Responsible);

            Cnpj = dto.Cnpj;
            Name = dto.Name;
            ParentId = dto.ParentId;
            SegmentId = dto.SegmentId;
            TradeName = dto.TradeName;
        }

        private CompanyResponsible CreateResponsible(Guid companyId, CompanyResponsibleDto dto)
        {
            dto.CompanyId = companyId;
            return CompanyResponsible.Create(dto);
        }

        public void Deactivate(Guid deactivationUserId)
        {
            DeactivationUserId = deactivationUserId;
            DeactivationDate = DateTime.UtcNow;
        }

        public void Activate()
        {
            DeactivationUserId = null;
            DeactivationDate = null;
        }

        public bool IsActive() => !DeactivationDate.HasValue;
        public bool IsInactive() => DeactivationDate.HasValue;
        public bool IsPrivate() => Segment.CompanyType.IsPrivate();
        public bool IsPrivateHeadquarters() => IsHeadquarters.GetValueOrDefault() && Segment.CompanyType.IsPrivate();
        public bool BranchCompanyCnpjIsValid(string cnpj)
            => Cnpj.Substring(0, 8) == cnpj.Substring(0, 8);
        public bool IsStateGovernment() => Segment.IsState() && Segment.IsGovernmentType();
        public bool IsFederalGovernment() => Segment.IsFederal() && Segment.IsGovernmentType();
        public bool IsMunicipalGovernment() => Segment.IsMunicipal() && Segment.IsGovernmentType();
        public void AddSubsidiaries(List<Company> subs)
        {
            if (Subsidiaries != null)
                subs.AddRange(Subsidiaries);

            Subsidiaries = subs;
        }
        public bool CanEditCompanyFields(CompanyEditDto dto, string loggedUserProfile)
        {
            if (loggedUserProfile == EProfileKey.government.ToString() &&
                    (ParentId != dto.ParentId
                        || SegmentId != dto.SegmentId
                        || Cnpj != dto.Cnpj
                        || Address?.CityId != dto.Address?.CityId))
                return false;

            return true;
        }
        public bool HasActiveSubsidiaries() => Subsidiaries != null && Subsidiaries.Any(x => x.IsActive());
    }
}
