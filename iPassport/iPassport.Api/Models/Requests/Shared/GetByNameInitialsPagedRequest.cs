namespace iPassport.Api.Models.Requests.Shared
{
    public class GetByNameInitialsPagedRequest : PageFilterRequest
    {
        public string Initals { get; set; }
    }
}
