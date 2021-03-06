namespace iPassport.Domain.Dtos
{
    public class StateCreateDto
    {
        public string Name { get;  set; }
        public string Acronym { get;  set; }
        public int IbgeCode { get;  set; }
        public System.Guid CountryId { get;  set; }
        public int? Population { get;  set; }
    }
}
