using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class UserVaccineMap : IEntityTypeConfiguration<UserVaccine>
    {
        public void Configure(EntityTypeBuilder<UserVaccine> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Dose)
                .IsRequired();

            builder.Property(c => c.VaccinationDate)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.ExclusionDate);

            builder.HasOne(c => c.Vaccine)
                .WithMany(c => c.UserVaccines)
                .HasForeignKey(c => c.VaccineId);

            builder.HasOne(c => c.UserDetails)
                .WithMany(c => c.UserVaccines)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.HealthUnit)
                .WithMany(c => c.UserVaccines)
                .HasForeignKey(c => c.HealthUnitId)
                .IsRequired(false);



        }
    }
}
