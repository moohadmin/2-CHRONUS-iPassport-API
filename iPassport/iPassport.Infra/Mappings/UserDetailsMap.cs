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

            builder.Property(x => x.FullName)
                .HasColumnType("nvarchar");

            builder.Property(x => x.CPF)
                .HasColumnType("nvarchar");

            builder.Property(x => x.CNS)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Birthday)
                .HasColumnType("DateTime");

            builder.Property(x => x.Gender)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Breed)
                .HasColumnType("nvarchar");

            builder.Property(x => x.BloodType)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Occupation)
                .HasColumnType("nvarchar(");

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
