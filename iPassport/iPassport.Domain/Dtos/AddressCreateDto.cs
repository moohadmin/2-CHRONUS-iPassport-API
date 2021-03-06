namespace iPassport.Domain.Dtos
{
    public class AddressCreateDto
    {
        public string Description { get; set; }
        public System.Guid CityId { get; set; }
        public string Cep { get; set; }
    }
}
