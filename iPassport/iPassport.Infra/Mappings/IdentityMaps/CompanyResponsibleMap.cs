using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class CompanyResponsibleMap : IEntityTypeConfiguration<CompanyResponsible>
    {
        public void Configure(EntityTypeBuilder<CompanyResponsible> builder)
        {
            builder.ToTable("CompanyResponsibles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Occupation);

            builder.Property(x => x.Email);

            builder.Property(x => x.MobilePhone);

            builder.Property(x => x.Landline);

            builder.Property(x => x.UpdateDate)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();
        }
    }
}
