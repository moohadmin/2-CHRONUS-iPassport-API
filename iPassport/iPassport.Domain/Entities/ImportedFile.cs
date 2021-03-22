using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace iPassport.Domain.Entities
{
    public class ImportedFile : Entity
    {
        public ImportedFile() { }

        public string Name { get; private set; }        
        public int TotalLines { get; private set; }
        public Guid UserId { get; private set; }
       

        public UserDetails ImportingUser { get; set;}
        public virtual IEnumerable<ImportedFileDetails> ImportedFileDetails { get; set; }

        public ImportedFile Create() => new ImportedFile();
        
        public int TotalLinesImportedSuccessfully() => TotalLines - ImportedFileDetails.Select(x => x.LineNumber).Distinct().Count();
        public int TotalLinesImportedError() => ImportedFileDetails.Select(x => x.LineNumber).Distinct().Count();
        public int Status()
        {
            if (!ImportedFileDetails.Any())
                return (int)EImportedFileStatus.Success;
            if(TotalLinesImportedError() == TotalLines)
                return (int)EImportedFileStatus.Error;

            return (int)EImportedFileStatus.Partial;
        }
    }
}
