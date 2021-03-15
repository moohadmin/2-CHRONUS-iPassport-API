using iPassport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPassport.Infra.Mappings
{
    public class UserDiseaseTestMap : IEntityTypeConfiguration<UserDiseaseTest>
    {
        public void Configure(EntityTypeBuilder<UserDiseaseTest> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.Result);

            builder.Property(c => c.ResultDate);

            builder.Property(c => c.TestDate)
                .IsRequired();

            builder.HasOne(c => c.Disease)
                .WithMany(p => p.UserDiseaseTests)
                .HasForeignKey(c => c.DiseaseId);

            builder.HasOne(c => c.User)
                .WithMany(p => p.UserDiseaseTests)
                .HasForeignKey(c => c.UserId);
        }
    }
}
