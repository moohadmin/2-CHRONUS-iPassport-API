namespace iPassport.Domain.Dtos
{
    public class CompanyCreateDto : CompanyAbstractDto
    {
        public AddressCreateDto Address { get; set; }
        public CompanyResponsibleCreateDto Responsible { get; set; }
    }
}
