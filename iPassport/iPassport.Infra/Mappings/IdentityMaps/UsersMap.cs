using iPassport.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class UsersMap : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(d => d.CPF).IsUnique();
            builder.HasIndex(d => d.CNS).IsUnique();
            builder.HasIndex(d => d.RG).IsUnique();
            builder.HasIndex(d => d.PassportDoc).IsUnique();
            builder.HasIndex(d => d.InternationalDocument).IsUnique();
            builder.HasIndex(t => t.PhoneNumber).IsUnique();

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId);
            
            builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(false);
        }
    }
}
