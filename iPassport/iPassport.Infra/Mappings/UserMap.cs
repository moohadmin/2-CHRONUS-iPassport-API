using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Username)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar");

            builder.Property(x => x.PasswordIsValid)
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(x => x.Email)
                .HasColumnType("nvarchar(128)");

            builder.Property(x => x.Mobile)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.Profile)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.Role)
                .HasColumnType("nvarchar(20)");

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            /// Relations
            builder
                .HasOne(x => x.UserDetails)
                .WithOne(y => y.User);
        }
    }
}
