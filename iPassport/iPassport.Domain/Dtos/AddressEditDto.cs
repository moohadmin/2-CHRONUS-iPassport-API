namespace iPassport.Domain.Dtos
{
    public class AddressEditDto
    {
        public System.Guid Id { get; set; }
        public string Description { get; set; }
        public System.Guid CityId { get; set; }
        public string Cep { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
    }
}
