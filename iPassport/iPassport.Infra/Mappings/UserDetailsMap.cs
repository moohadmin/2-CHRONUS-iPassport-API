using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class UserDetailsMap : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.Bond);

            builder.Property(c => c.PriorityGroup);

            builder.HasOne(c => c.Plan)
                .WithMany(p => p.Users);

            builder.HasOne(c => c.PPriorityGroup)                
                .WithMany()
                .HasForeignKey(x => x.PriorityGroupId)
                .IsRequired(false);
        }
    }
}
