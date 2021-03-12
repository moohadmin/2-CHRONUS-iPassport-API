using System;

namespace iPassport.Api.Models.Requests.User
{
    public class GetPagedUserVaccinesByPassportRequest : PageFilterRequest
    {
        public Guid PassportId { get; set; }
    }
}
