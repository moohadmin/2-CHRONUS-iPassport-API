using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class PassportDetailsMap : IEntityTypeConfiguration<PassportDetails>
    {

        public void Configure(EntityTypeBuilder<PassportDetails> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(x => x.ExpirationDate)
                .IsRequired();
        }

    }
}
