namespace iPassport.Domain.Dtos
{
    public class CountryCreateDto
    {
        public string Name { get;  set; }
        public string Acronym { get;  set; }
        public string ExternalCode { get;  set; }
        public long? Population { get; set; }
    }
}
