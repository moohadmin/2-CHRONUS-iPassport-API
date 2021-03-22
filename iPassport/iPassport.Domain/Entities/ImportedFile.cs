using iPassport.Domain.Dtos;
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

        public ImportedFile (string name, int totalLines, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            TotalLines = totalLines;
            UserId = userId;
        }


        public UserDetails ImportingUser { get; set;}
        public virtual IList<ImportedFileDetails> ImportedFileDetails { get; set; }

        public ImportedFile Create() => new ImportedFile();
        
        public int TotalLinesImportedSuccessfully() => TotalLines - ImportedFileDetails.Count();
        public int TotalLinesImportedError() => ImportedFileDetails.Count();
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
