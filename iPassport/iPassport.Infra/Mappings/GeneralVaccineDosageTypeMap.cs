using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class GeneralVaccineDosageTypeMap : IEntityTypeConfiguration<GeneralVaccineDosageType>
    {
        public void Configure(EntityTypeBuilder<GeneralVaccineDosageType> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.RequiredDoses)
                .IsRequired();

            builder.Property(p => p.MaxTimeNextDose)
                .IsRequired();

            builder.Property(p => p.MinTimeNextDose)
                .IsRequired();

            builder.Property(p => p.PeriodTypeId)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.VaccineId)
                .IsRequired();

            builder.HasOne(c => c.PeriodType)
                .WithMany(c => c.GeneralVaccineDosageTypes)
                .HasForeignKey(c => c.PeriodTypeId);

            builder.HasOne(c => c.Vaccine)
                .WithOne(c => c.GeneralVaccineDosage);
        }
    }
}
