using iPassport.Domain.Entities;
using iPassport.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace iPassport.Infra.Contexts
{
    public class iPassportContext : DbContext
    {
        public iPassportContext(DbContextOptions<iPassportContext> options) : base(options) { }
        public iPassportContext() { }

        /// <summary>
        /// Declaração das Entidades para o EntityFramework
        /// </summary>
        public DbSet<Health> Healths { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Auth2FactMobile> Auth2FactMobile { get; set; }

        /// <summary>
        ///  Usado para aplicar os Mappings das Entidades
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthMap());
            modelBuilder.ApplyConfiguration(new UserDetailsMap());
            modelBuilder.ApplyConfiguration(new PlanMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
