namespace iPassport.Api.Models.Requests.Shared
{
    public class GetByNamePartsPagedRequest : PageFilterRequest
    {
        public string Initials { get; set; }
    }
}
