using System;

namespace iPassport.Domain.Dtos
{
    public abstract class CompanyAbstractDto
    {
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public Guid SegmentId { get; set; }
        public bool? IsHeadquarters { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsActive { get; set; }

    }
}
