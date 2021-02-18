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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Type)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal");

            builder.HasMany(p => p.Users)
                .WithOne(u => u.Plan)
                .HasForeignKey(u => u.PlanId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
