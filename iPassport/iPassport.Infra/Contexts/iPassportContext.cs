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
        public DbSet<HealthUnit> HealthUnits { get; set; }
        public DbSet<HealthUnitType> HealthUnitTypes { get; set; }
        public DbSet<UserDiseaseTest> UserDiseaseTests { get; set; }
        public DbSet<PriorityGroup> PriorityGroups { get; set; }
        public DbSet<ImportedFile> ImportedFiles { get; set; }
        public DbSet<ImportedFileDetails> ImportedFileDetails { get; set; }
        public DbSet<VaccineDosageType> VaccineDosageTypes { get; set; }
        public DbSet<VaccinePeriodType> VaccinePeriodTypes { get; set; }
        public DbSet<AgeGroupVaccineDosageType> AgeGroupVaccineDosageTypes { get; set; }
        public DbSet<GeneralVaccineDosageType> GeneralVaccineDosageTypes { get; set; }

        /// <summary>
        ///  Usado para aplicar os Mappings das Entidades
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDetailsMap());
            modelBuilder.ApplyConfiguration(new PlanMap());
            modelBuilder.ApplyConfiguration(new PassportMap());
            modelBuilder.ApplyConfiguration(new PassportDetailsMap());
            modelBuilder.ApplyConfiguration(new DiseaseMap());
            modelBuilder.ApplyConfiguration(new VaccineMap());
            modelBuilder.ApplyConfiguration(new UserVaccineMap());
            modelBuilder.ApplyConfiguration(new PassportUseMap());
            modelBuilder.ApplyConfiguration(new VaccineManufacterMap());
            modelBuilder.ApplyConfiguration(new HealthUnitMap());
            modelBuilder.ApplyConfiguration(new HealthUnitTypeMap());
            modelBuilder.ApplyConfiguration(new UserDiseaseTestMap());
            modelBuilder.ApplyConfiguration(new PriorityGroupMap());
            modelBuilder.ApplyConfiguration(new ImportedFileMap());
            modelBuilder.ApplyConfiguration(new ImportedFileDetailsMap());
            modelBuilder.ApplyConfiguration(new VaccineDosageTypeMap());
            modelBuilder.ApplyConfiguration(new VaccinePeriodTypeMap());
            modelBuilder.ApplyConfiguration(new AgeGroupVaccineDosageTypeMap());
            modelBuilder.ApplyConfiguration(new GeneralVaccineDosageTypeMap());

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
