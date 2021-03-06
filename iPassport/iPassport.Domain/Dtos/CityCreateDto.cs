namespace iPassport.Domain.Dtos
{
    public class CityCreateDto
    {
        public string Name { get;  set; }
        public int IbgeCode { get;  set; }
        public System.Guid StateId { get;  set; }
        public int? Population { get;  set; }
    }
}
