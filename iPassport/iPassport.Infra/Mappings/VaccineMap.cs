using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class VaccineMap : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Laboratory)
                .HasColumnType("nvarchar(80)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(p => p.ExpirationTime)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.ImunizationTime)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.RequiredDoses)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.HasMany(c => c.Diseases)
                .WithMany(c => c.Vaccines);

        }
    }
}
