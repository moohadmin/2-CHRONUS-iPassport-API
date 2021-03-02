using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class PassportMap : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(x => x.PassId)
                .UseIdentityAlwaysColumn()
                .IsRequired();

            builder.HasOne(x => x.UserDetails)
                    .WithOne(x => x.Passport)
                    .HasForeignKey<Passport>(x => x.UserDetailsId);

            builder.HasMany(x => x.ListPassportDetails)
                    .WithOne(x => x.Passport)
                    .HasForeignKey(x => x.PassportId);

        }
    }
}
