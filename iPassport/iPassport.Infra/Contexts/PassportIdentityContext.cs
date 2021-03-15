using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Infra.Mappings.IdentityMaps;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace iPassport.Infra.Contexts
{
    public class PassportIdentityContext : IdentityDbContext<Users, Roles, Guid>
    {

        public PassportIdentityContext(DbContextOptions<PassportIdentityContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<UserToken> AppUserTokens { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsersMap());
            
            builder.ApplyConfiguration(new UserTokenMap());
            
            builder.Entity<Roles>().ToTable("Roles");

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");

            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");

            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");

            builder.ApplyConfiguration(new AddressMap());

            builder.ApplyConfiguration(new CityMap());

            builder.ApplyConfiguration(new StateMap());

            builder.ApplyConfiguration(new CountryMap());

            builder.ApplyConfiguration(new CompanyMap());

            builder.ApplyConfiguration(new GenderMap());

            //To avoid delete cascade.
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }
    }
}
