namespace iPassport.Domain.Filters
{
    public class GetByIdAndNamePartsPagedFilter : PageFilter
    {
        public string Initials { get; set; }
        public System.Guid Id { get; set; }
    }
}
