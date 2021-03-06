using iPassport.Domain.Entities;

namespace iPassport.Domain.Dtos
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public System.Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
