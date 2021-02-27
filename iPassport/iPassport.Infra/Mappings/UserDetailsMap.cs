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

            builder.Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.HasOne(c => c.Plan)
                .WithMany(p => p.Users);
        }
    }
}
