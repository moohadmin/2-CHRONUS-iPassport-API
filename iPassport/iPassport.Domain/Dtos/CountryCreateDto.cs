namespace iPassport.Domain.Dtos
{
    public class CountryCreateDto
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }
    }
}
