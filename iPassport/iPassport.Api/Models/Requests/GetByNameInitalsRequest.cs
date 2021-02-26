namespace iPassport.Api.Models.Requests
{
    public class GetByNameInitalsRequest : PageFilterRequest
    {
        public string Initials { get; set; }
    }
}
