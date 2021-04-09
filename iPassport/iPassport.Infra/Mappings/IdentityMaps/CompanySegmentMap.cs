using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class CompanySegmentMap : IEntityTypeConfiguration<CompanySegment>
    {
        public void Configure(EntityTypeBuilder<CompanySegment> builder)
        {
            builder.ToTable("CompanySegments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Identifyer)
                .IsRequired();

            builder.HasIndex(x => x.Identifyer)
                .IsUnique();

            builder.HasOne(x => x.CompanyType)
                .WithMany()
                .HasForeignKey(x => x.CompanyTypeId);
        }
    }
}
