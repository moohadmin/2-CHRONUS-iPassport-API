using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings.IdentityMaps
{
    public class HumanRaceMap : IEntityTypeConfiguration<HumanRace>
    {
        public void Configure(EntityTypeBuilder<HumanRace> builder)
        {
            builder.ToTable("HumanRaces");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
