using System;

namespace iPassport.Domain.Dtos
{
    public class CompanyEditDto : CompanyAbstractDto
    {
        public Guid Id { get; set; }
        public AddressEditDto Address { get; set; }
        public CompanyResponsibleEditDto Responsible { get; set; }
    }
}
