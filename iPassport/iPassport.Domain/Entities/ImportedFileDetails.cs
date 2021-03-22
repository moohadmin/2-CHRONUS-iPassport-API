using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class ImportedFileDetails : Entity
    {
        public ImportedFileDetails() { }

        public int LineNumber { get; private set; }
        public string FieldName { get; private set; }
        public string ErrorDescription { get; private set; }
        public Guid ImportedFileId { get; private set; }

        public ImportedFile ImportedFile { get; set; }
        public ImportedFileDetails Create() => new ImportedFileDetails();
    }
}
