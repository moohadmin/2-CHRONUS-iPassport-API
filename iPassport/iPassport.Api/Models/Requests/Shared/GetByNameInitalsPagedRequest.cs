namespace iPassport.Api.Models.Requests.Shared
{
    public class GetByNameInitalsPagedRequest : PageFilterRequest
    {
        public string Initals { get; set; }
    }
}
