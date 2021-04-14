namespace iPassport.Domain.Dtos
{
    public class CompanyCreateDto : CompanyAbstractDto
    {
        public AddressCreateDto Address { get; set; }
        public CompanyResponsibleDto Responsible { get; set; }
    }
}
