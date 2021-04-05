using System;

namespace iPassport.Domain.Filters
{
    public class GetAdminUserPagedFilter : PageFilter
    {
        public string Initials { get; set; }
        public string Cpf { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ProfileId { get; set; }
    }
}
