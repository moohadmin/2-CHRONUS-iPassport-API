namespace iPassport.Domain.Dtos
{
    public class CountryCreateDto
    {
        public string Name { get;  set; }
        public string Acronym { get;  set; }
        public int IbgeCode { get;  set; }
        public int? Population { get; set; }
    }
}
