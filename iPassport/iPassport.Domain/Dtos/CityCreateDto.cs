namespace iPassport.Domain.Dtos
{
    public class CityCreateDto
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public int IbgeCode { get; private set; }
        public System.Guid StateId { get; private set; }
    }
}
