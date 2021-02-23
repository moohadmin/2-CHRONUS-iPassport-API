using iPassport.Domain.Entities;
using iPassport.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace iPassport.Infra.Contexts
{
    public class iPassportContext : DbContext
    {
        public iPassportContext(DbContextOptions options) : base(options) { }
        public iPassportContext() { }

        /// <summary>
        /// Declaração das Entidades para o EntityFramework
        /// </summary>
        public DbSet<Health> Healths { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<UserVaccine> UserVaccines { get; set; }

        /// <summary>
        ///  Usado para aplicar os Mappings das Entidades
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthMap());
            modelBuilder.ApplyConfiguration(new UserDetailsMap());
            modelBuilder.ApplyConfiguration(new PlanMap());
            modelBuilder.ApplyConfiguration(new DiseaseMap());
            modelBuilder.ApplyConfiguration(new VaccineMap());
            modelBuilder.ApplyConfiguration(new UserVaccineMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
