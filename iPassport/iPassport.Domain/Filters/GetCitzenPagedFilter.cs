using iPassport.Domain.Enums;

namespace iPassport.Domain.Filters
{
    public class GetCitzenPagedFilter : PageFilter
    {
        public string Initials { get; set; }

        public EDocumentType? DocumentType { get; set; }

        public string Document { get; set; }

        public string Telephone { get; set; }
    }
}
