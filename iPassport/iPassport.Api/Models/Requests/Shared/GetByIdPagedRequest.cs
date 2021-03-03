using System;

namespace iPassport.Api.Models.Requests.Shared
{
    public class GetByIdPagedRequest : PageFilterRequest
    {
        public Guid Id { get; set; }
    }
}
