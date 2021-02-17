using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Type)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(c => c.Price)
                .HasColumnType("decimal");
        }
    }
}
