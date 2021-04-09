using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Cnpj)
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId);

            builder.HasIndex(x => x.Cnpj);

            builder.HasOne(x => x.Segment)
                .WithMany()
                .HasForeignKey(x => x.SegmentId)
                .IsRequired(false);
        }
    }
}
