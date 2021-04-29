using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class UserUserTypeMap : IEntityTypeConfiguration<UserUserType>
    {
        public void Configure(EntityTypeBuilder<UserUserType> builder)
        {
            builder.ToTable("UserUserTypes");

            builder.HasKey(p => new {p.UserId, p.UserTypeId });

            builder.Property(p => p.DeactivationUserId)
                .IsRequired(false);

            builder.Property(p => p.DeactivationDate)
                .IsRequired(false);

            builder.HasOne(x => x.UserType)
                .WithMany(x => x.UserUserTypes)
                .HasForeignKey(x => x.UserTypeId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserUserTypes)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.DeactivationUser)
                .WithMany()
                .HasForeignKey(x => x.DeactivationUserId)
                .IsRequired(false);
            
        }
    }
}
