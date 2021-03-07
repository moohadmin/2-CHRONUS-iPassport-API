using iPassport.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("AppUserTokens");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Provider)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.Provider, x.Value }).IsUnique();
            builder.HasIndex(e => e.Value);
        }
    }
}
