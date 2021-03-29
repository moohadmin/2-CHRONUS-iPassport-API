using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class HealthUnitMap : IEntityTypeConfiguration<HealthUnit>
    {
        public void Configure(EntityTypeBuilder<HealthUnit> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(t => t.Ine)
                .IsUnique();

            builder.HasIndex(t => t.UniqueCode)
                .IsUnique();

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Cnpj);

            builder.Property(p => p.Ine);

            builder.Property(p => p.Active);

            builder.Property(p => p.Email);

            builder.Property(p => p.ResponsiblePersonName);

            builder.Property(p => p.ResponsiblePersonPhone);

            builder.Property(p => p.ResponsiblePersonOccupation);

            builder.Property(p => p.DeactivationDate);

            builder.Property(p => p.AddressId);

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.HasOne(x => x.Type)
                .WithMany()
                .HasForeignKey(x => x.TypeId);
        }
    }
}
