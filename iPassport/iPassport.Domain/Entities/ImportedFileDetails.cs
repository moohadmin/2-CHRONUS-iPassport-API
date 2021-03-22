using System;

namespace iPassport.Domain.Entities
{
    public class ImportedFileDetails : Entity
    {
        public ImportedFileDetails() { }

        public int LineNumber { get; private set; }
        public string FieldName { get; private set; }
        public string ErrorDescription { get; private set; }
        public Guid ImportedFileId { get; private set; }

        public virtual ImportedFile ImportedFile { get; set; }
        public ImportedFileDetails Create() => new ImportedFileDetails();
    }
}
