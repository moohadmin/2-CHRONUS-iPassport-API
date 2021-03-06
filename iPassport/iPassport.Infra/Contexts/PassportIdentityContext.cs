using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Infra.Mappings.IdentityMaps;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace iPassport.Infra.Contexts
{
    public class PassportIdentityContext : IdentityDbContext<Users, Roles, Guid>
    {
        public PassportIdentityContext(DbContextOptions<PassportIdentityContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsersMap());

            //builder.Entity<Users>().ToTable("Users");

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

        }
    }
}
