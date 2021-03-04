using iPassport.Domain.Entities;
using iPassport.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iPassport.Infra.Contexts
{
    public class iPassportContext : DbContext
    {
        public iPassportContext(DbContextOptions<iPassportContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public iPassportContext() { }

        /// <summary>
        /// Declaração das Entidades para o EntityFramework
        /// </summary>
        public DbSet<Health> Healths { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Auth2FactMobile> Auth2FactMobile { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<PassportDetails> PassportDetails { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<UserVaccine> UserVaccines { get; set; }
        public DbSet<PassportUse> PassportUses { get; set; }
        public DbSet<VaccineManufacturer> VaccineManufacturers { get; set; }

        /// <summary>
        ///  Usado para aplicar os Mappings das Entidades
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new HealthMap());
            modelBuilder.ApplyConfiguration(new UserDetailsMap());
            modelBuilder.ApplyConfiguration(new PlanMap());
            modelBuilder.ApplyConfiguration(new PassportMap());
            //modelBuilder.ApplyConfiguration(new PassportDetailsMap());
            //modelBuilder.ApplyConfiguration(new DiseaseMap());
            modelBuilder.ApplyConfiguration(new VaccineMap());
            modelBuilder.ApplyConfiguration(new UserVaccineMap());
            //modelBuilder.ApplyConfiguration(new PassportUseMap());
            //modelBuilder.ApplyConfiguration(new VaccineManufacterMap());

            //To avoid delete cascade.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
