using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class ImportedFileMap : IEntityTypeConfiguration<ImportedFile>
    {
        public void Configure(EntityTypeBuilder<ImportedFile> builder)
        {
            builder.ToTable("ImportedFiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.TotalLines)
                .IsRequired();

            builder.HasOne(x => x.ImportingUser)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
