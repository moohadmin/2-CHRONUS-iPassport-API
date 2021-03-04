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
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Price);

            builder.Property(p => p.Observation);

            builder.Property(p => p.ColorStart)
                .IsRequired();
            
            builder.Property(p => p.ColorEnd)
                .IsRequired();

            builder.Property(p => p.Active)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();
        }
    }
}
