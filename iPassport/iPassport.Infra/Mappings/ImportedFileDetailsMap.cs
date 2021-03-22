using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class ImportedFileDetailsMap : IEntityTypeConfiguration<ImportedFileDetails>
    {
        public void Configure(EntityTypeBuilder<ImportedFileDetails> builder)
        {
            builder.ToTable("ImportedFileDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LineNumber)
                .IsRequired();

            builder.Property(x => x.FieldName);

            builder.Property(x => x.ErrorDescription)
                .IsRequired();

            builder.HasOne(x => x.ImportedFile)
                .WithMany(x => x.ImportedFileDetails)
                .HasForeignKey(x => x.ImportedFileId)
                .IsRequired();
        }
    }
}
