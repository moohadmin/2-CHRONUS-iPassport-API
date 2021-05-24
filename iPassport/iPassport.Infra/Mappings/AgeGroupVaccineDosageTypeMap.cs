using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class AgeGroupVaccineDosageTypeMap : IEntityTypeConfiguration<AgeGroupVaccine>
    {
        public void Configure(EntityTypeBuilder<AgeGroupVaccine> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.InitalAgeGroup)
                .IsRequired();

            builder.Property(p => p.FinalAgeGroup)
                .IsRequired();

            builder.Property(p => p.MaxTimeNextDose);

            builder.Property(p => p.MinTimeNextDose)
                .IsRequired();

            builder.Property(p => p.RequiredDoses)
                .IsRequired();

            builder.Property(p => p.PeriodTypeId);

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.VaccineId)
                .IsRequired();

            builder.HasOne(c => c.PeriodType)
                .WithMany(c => c.AgeGroupVaccines)
                .HasForeignKey(c => c.PeriodTypeId);

            builder.HasOne(c => c.Vaccine)
                .WithMany(c => c.AgeGroupVaccines)
                .HasForeignKey(c => c.VaccineId);
        }
    }
}
