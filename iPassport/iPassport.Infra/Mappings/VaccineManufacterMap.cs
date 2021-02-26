using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class VaccineManufacterMap : IEntityTypeConfiguration<VaccineManufacturer>
    {
        public void Configure(EntityTypeBuilder<VaccineManufacturer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.HasMany(c => c.Vaccines)
                .WithOne(c => c.Manufacturer)
                .HasForeignKey(c => c.ManufacturerId);
        }
    }
}
