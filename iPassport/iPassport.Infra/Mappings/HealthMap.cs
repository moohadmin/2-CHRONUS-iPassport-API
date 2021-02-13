using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class HealthMap : IEntityTypeConfiguration<Health>
    {
        public void Configure(EntityTypeBuilder<Health> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Status)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();
        }
    }
}
