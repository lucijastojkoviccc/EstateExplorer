using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using EstateExplorer.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace EstateExplorer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IPersistedGrantDbContext//ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Nedostupnost> Nedostupnosti { get; set; }
        public DbSet<Nekretnina> Nekretnine { get; set; }
        public DbSet<Obavestenja> Obavestenja { get; set; }
        public DbSet<Rata> Rate { get; set; }
        public DbSet<Stan> Stanovi { get; set; }
        public DbSet<VidjenoObavestenje> VidjenaObavestenja { get; set; }
        public DbSet<ZakazivanjeTermina> ZakazivanjeTermina { get; set; }
        public DbSet<Zgrada> Zgrade { get; set; }
        public DbSet<CurrencyValues> CurrencyValues { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get ; set; }
        public DbSet<Key> Keys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            modelBuilder.Entity<CurrencyValues>().HasIndex(cv => cv.Code).IsUnique();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        }

    }
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Ime).HasMaxLength(255);
            builder.Property(u => u.Prezime).HasMaxLength(255);
        }
    }
}
