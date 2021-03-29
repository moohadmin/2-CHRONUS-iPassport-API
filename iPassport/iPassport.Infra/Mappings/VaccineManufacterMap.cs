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
            .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.HasMany(c => c.Vaccines)
                .WithOne(c => c.Manufacturer)
                .HasForeignKey(c => c.ManufacturerId);

            builder.HasIndex(x => x.Name);
        }
    }
}
