using System;

namespace iPassport.Domain.Filters
{
    public class GetAdminUserPagedFilter : PageFilter
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ProfileId { get; set; }
    }
}
