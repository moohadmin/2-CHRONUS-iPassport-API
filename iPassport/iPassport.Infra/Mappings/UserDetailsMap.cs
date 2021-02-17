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

            builder.HasOne(x => x.User)
                .WithOne(s => s.UserDetails)
                .HasForeignKey<UserDetails>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasColumnType("nvarchar(128)");

            builder.Property(x => x.CPF)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.CNS)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.Birthday)
                .HasColumnType("DateTime");

            builder.Property(x => x.Gender)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.Breed)
                .HasColumnType("nvarchar(20)");

            builder.Property(x => x.BloodType)
                .HasColumnType("nvarchar(5)");

            builder.Property(x => x.Occupation)
                .HasColumnType("nvarchar(128)");

            builder.Property(x => x.Address)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Photo)
                .HasColumnType("nvarchar");

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();
        }
    }
}
