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

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.ExpirationTime)
                .IsRequired();

            builder.Property(p => p.ImunizationTime)
                .IsRequired();

            builder.Property(p => p.RequiredDoses)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.MaxTimeNextDose)
                .IsRequired();

            builder.Property(c => c.MinTimeNextDose)
                .IsRequired();

            builder.HasMany(c => c.Diseases)
                .WithMany(c => c.Vaccines);

            builder.HasOne(c => c.Manufacturer)
                .WithMany(c => c.Vaccines)
                .HasForeignKey(c => c.ManufacturerId);
        }
    }
}
