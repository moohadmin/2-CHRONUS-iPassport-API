using System;

namespace iPassport.Application.Models.ViewModels
{
    public class ImportedFileDetailsViewModel
    {
        public int LineNumber { get; set; }
        public string FieldName { get; set; }
        public string ErrorDescription { get; set; }
        public Guid ImportedFileId { get; set; }
    }
}
