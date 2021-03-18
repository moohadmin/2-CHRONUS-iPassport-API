using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Adresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Cep)
                .IsRequired(false);

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.District)
                .IsRequired(false);

            builder.Property(x => x.Number)
                .IsRequired(false);

            builder.Property(x => x.UpdateDate)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();

            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .IsRequired();
        }
    }
}
