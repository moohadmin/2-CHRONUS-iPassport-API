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
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.VaccinationDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.HasOne(c => c.Vaccine)
                .WithMany(c => c.UserVaccines)
                .HasForeignKey(c => c.VaccineId);

            builder.HasOne(c => c.UserDetails)
                .WithMany(c => c.UserVaccines)
                .HasForeignKey(c => c.UserId);

        }
    }
}
