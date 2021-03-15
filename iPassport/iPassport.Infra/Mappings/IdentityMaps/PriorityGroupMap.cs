using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class PriorityGroupMap : IEntityTypeConfiguration<PriorityGroup>
    {
        public void Configure(EntityTypeBuilder<PriorityGroup> builder)
        {
            builder.ToTable("PriorityGroups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
