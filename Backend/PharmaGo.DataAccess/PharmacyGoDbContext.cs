using Microsoft.EntityFrameworkCore;
using PharmaGo.Domain.Entities;
using System.Numerics;

namespace PharmaGo.DataAccess
{
    public class PharmacyGoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Pharmacy> Pharmacys { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StockRequest> StockRequests { get; set; }
        public DbSet<StockRequestDetail> StockRequestDetails { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public PharmacyGoDbContext(DbContextOptions<PharmacyGoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePrecisions(modelBuilder);
            ConfigureConversions(modelBuilder);
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "Owner" },
                new Role { Id = 3, Name = "Employee" }
            );

            modelBuilder.Entity<UnitMeasure>().HasData(
                new UnitMeasure { Id = 1, Name = "mg" },
                new UnitMeasure { Id = 2, Name = "g" },
                new UnitMeasure { Id = 3, Name = "ml" },
                new UnitMeasure { Id = 4, Name = "L" },
                new UnitMeasure { Id = 5, Name = "unit" }
            );

            modelBuilder.Entity<Presentation>().HasData(
                new Presentation { Id = 1, Name = "Tablet" },
                new Presentation { Id = 2, Name = "Syrup" },
                new Presentation { Id = 3, Name = "Injection" },
                new Presentation { Id = 4, Name = "Cream" },
                new Presentation { Id = 5, Name = "Ointment" },
                new Presentation { Id = 6, Name = "Capsule" },
                new Presentation { Id = 7, Name = "Powder" },
                new Presentation { Id = 8, Name = "Patch" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, UserName = "Admin", Email = "admin@admin.com", Password = "Admin123*",
                    Address = "Admin Address",
                    RegistrationDate = DateTime.MinValue, RoleId = 1
                }
            );
        }

        private static void ConfigurePrecisions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drug>().Property(property => property.Price).HasPrecision(14, 2);
            modelBuilder.Entity<Purchase>().Property(property => property.TotalAmount).HasPrecision(14, 2);
            modelBuilder.Entity<PurchaseDetail>().Property(property => property.Price).HasPrecision(14, 2);
        }

        private static void ConfigureConversions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitMeasure>().Property(u => u.Name).HasConversion<string>();
            modelBuilder.Entity<Presentation>().Property(u => u.Name).HasConversion<string>();
        }
    }
}