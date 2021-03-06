using iPassport.Domain.Dtos;

namespace iPassport.Domain.Entities
{
    public class Company : Entity
    {
        public Company() { }
        public Company(string name, string cnpj, System.Guid addressId)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Cnpj = cnpj;
            AddressId = addressId;
        }

        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public System.Guid AddressId { get; private set; }

        public Address Address { get; set; }

        public Company Create(CompanyCreateDto dto) => new Company(dto.Name, dto.Cnpj, dto.AddressId);


    }
}
