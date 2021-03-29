using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class ImportedFileSeed
    {
        public static ImportedFile Get()
        {
            ImportedFile file = new("Arquivo 1", 10, Guid.NewGuid());
            file.ImportedFileDetails = new List<ImportedFileDetails>()
            {
                new ImportedFileDetails("teste","teste",3, Guid.NewGuid())
            };
            return file;
        }

        public static IList<ImportedFile> GetImportedFiles()
        {
            return new List<ImportedFile>()
            {
                Get()
            };
        }

        public static PagedData<ImportedFile> GetPaged()
        {
            return new PagedData<ImportedFile>() { Data = GetImportedFiles() };
        }

        public static IList<ImportedFileDetails> GetImportedFileDetails()
        {
            return new List<ImportedFileDetails>()
            {
                new ImportedFileDetails("teste","teste",3, Guid.NewGuid()),
                new ImportedFileDetails("teste2","teste2",3, Guid.NewGuid()),
                new ImportedFileDetails("teste2","teste2",3, Guid.NewGuid())
            };
        }
    }
}