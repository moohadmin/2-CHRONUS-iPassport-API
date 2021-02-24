using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class PassportUseMap : IEntityTypeConfiguration<PassportUse>
    {
        public void Configure(EntityTypeBuilder<PassportUse> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(x => x.AllowAccess)
                .HasColumnType("bit")                
                .IsRequired();

            builder.Property(x => x.Latitude)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(x => x.Longitude)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.HasOne(x => x.PassportDetails)
                .WithMany(x => x.PassportUses)
                .HasForeignKey(x => x.PassportDetailsId)
                .IsRequired();

            builder.HasOne(x => x.Agent)
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            builder.HasOne(x => x.Citizen)
                .WithMany()
                .HasForeignKey(x => x.CitizenId)                
                .OnDelete(DeleteBehavior.ClientNoAction)
                .IsRequired();
        }
    }
}
