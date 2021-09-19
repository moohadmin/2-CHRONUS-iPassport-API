using System;
using System.Collections.Generic;

namespace iPassport.Domain.Filters
{
    public class GetHealthUnitPagedFilter : GetByNamePartsPagedFilter
    {
        public string Cnpj { get; set; }
        public string Ine { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? TypeId { get; set; }
        public IList<Guid> Locations { get; set; }
    }
}
