using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Filters
{
    public class GetAgentPagedFilter : PageFilter
    {
        public string Login { get; set; }

        public string Initials { get; set; }

        public string Cpf { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid? CityId { get; set; }

        public Guid? StateId { get; set; }

        public Guid? CountryId { get; set; }
    }
}
