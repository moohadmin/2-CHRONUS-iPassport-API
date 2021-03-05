using System;

namespace iPassport.Api.Models.Requests.User
{
    public class GetPagedUserVaccinesRequest : PageFilterRequest
    {
        public Guid PassportId { get; set; }
    }
}
