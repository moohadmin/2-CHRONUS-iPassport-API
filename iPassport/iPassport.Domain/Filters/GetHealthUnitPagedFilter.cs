using System;

namespace iPassport.Domain.Filters
{
    public class GetHealthUnitPagedFilter : GetByNamePartsPagedFilter
    {
        public string Cnpj { get; set; }
        public string Ine { get; set; }
    }
}
