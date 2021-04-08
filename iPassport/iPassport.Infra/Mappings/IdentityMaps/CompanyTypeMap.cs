using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    /// <summary>
    /// Mapping dos campos da Entidade
    /// </summary>
    public class CompanyTypeMap : IEntityTypeConfiguration<CompanyType>
    {
        public void Configure(EntityTypeBuilder<CompanyType> builder)
        {
            builder.ToTable("CompanyTypes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Identifyer)
                .IsRequired();

            builder.HasIndex(x => x.Identifyer)
                .IsUnique();
        }
    }
}
