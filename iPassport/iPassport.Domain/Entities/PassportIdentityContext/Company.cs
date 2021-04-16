using iPassport.Domain.Dtos;
using iPassport.Domain.Entities.Authentication;
using System;

namespace iPassport.Domain.Entities
{
    public class Company : Entity
    {
        public Company() { }
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

        public Address Address { get; set; }
        public CompanySegment Segment { get; set; }
        public Company ParentCompany { get; set; }
        public CompanyResponsible Responsible { get; set; }
        public Users DeactivationUser { get; set; }

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
        public bool IsPrivateHeadquarters() => IsHeadquarters.GetValueOrDefault() && Segment.CompanyType.IsPrivate();
        public bool BranchCompanyCnpjIsValid(string cnpj)
            => Cnpj.Substring(0, 8) == cnpj.Substring(0, 8);
        public bool IsStateGovernment() => Segment.IsState() && Segment.IsGovernmentType();
        public bool IsFederalGovernment() => Segment.IsFederal() && Segment.IsGovernmentType();
        public bool IsMunicipalGovernment() => Segment.IsMunicipal() && Segment.IsGovernmentType();
    }
}
