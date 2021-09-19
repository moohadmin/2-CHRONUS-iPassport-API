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

            
            builder.Property(c => c.ImportedFileId);
            
            builder.Property(c => c.WasTestPerformed);

            builder.HasOne(c => c.Plan)
                .WithMany(p => p.Users);

            builder.HasOne(c => c.PriorityGroup)
                .WithMany()
                .HasForeignKey(x => x.PriorityGroupId)
                .IsRequired(false);

            builder.HasOne(c => c.ImportedFile)
                .WithMany()
                .HasForeignKey(x => x.ImportedFileId)
                .IsRequired(false);

            builder.HasOne(c => c.HealthUnit)
                .WithMany()
                .HasForeignKey(x => x.HealthUnitId)
                .IsRequired(false);
        }
    }
}
