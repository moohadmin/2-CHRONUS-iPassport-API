namespace iPassport.Domain.Dtos
{
    public class StateCreateDto
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }
        public System.Guid CountryId { get; private set; }
    }
}
