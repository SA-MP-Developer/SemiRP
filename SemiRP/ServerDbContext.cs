using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Dynamic;
using System.Text;
using Microsoft.Extensions.Configuration;
using SemiRP.Models.ItemHeritage;
using SemiRP.Models.ContainerHeritage;

namespace SemiRP
{
    public class ServerDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRank> GroupRanks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionSet> PermissionSets { get; set; }
        public DbSet<PermissionSetPermission> PermissionSetPermissions { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleData> Vehicles { get; set; }
        public DbSet<VehicleDataBorrower> VehicleDataBorrowers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Gun> Gun { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionSetPermission>()
                .HasKey(bc => new { bc.PermissionId, bc.PermissionSetId });
            modelBuilder.Entity<PermissionSetPermission>()
                .HasOne(bc => bc.Permission)
                .WithMany(b => b.PermissionsSetPermission)
                .HasForeignKey(bc => bc.PermissionId);
            modelBuilder.Entity<PermissionSetPermission>()
                .HasOne(bc => bc.PermissionSet)
                .WithMany(c => c.PermissionsSetPermission)
                .HasForeignKey(bc => bc.PermissionSetId);

            modelBuilder.Entity<VehicleDataBorrower>()
                .HasKey(bc => new { bc.BorrowerId, bc.VehicleId });
            modelBuilder.Entity<VehicleDataBorrower>()
                .HasOne(bc => bc.Borrower)
                .WithMany(b => b.BorrowedVehicles)
                .HasForeignKey(bc => bc.BorrowerId);
            modelBuilder.Entity<VehicleDataBorrower>()
                .HasOne(bc => bc.Vehicle)
                .WithMany(c => c.Borrowers)
                .HasForeignKey(bc => bc.VehicleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseMySql("",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 4, 12), ServerType.MariaDb))
                    .EnableDetailedErrors();

#else
                String sqlConString = $"Server={Environment.GetEnvironmentVariable("SEMIRP_MYSQL_HOST")}; " +
                    $"Port={Environment.GetEnvironmentVariable("SEMIRP_MYSQL_PORT")}; " +
                    $"Database={Environment.GetEnvironmentVariable("SEMIRP_MYSQL_DB")}; " +
                    $"Uid={Environment.GetEnvironmentVariable("SEMIRP_MYSQL_USER")}; " +
                    $"Pwd={Environment.GetEnvironmentVariable("SEMIRP_MYSQL_PASSWORD")};";
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseMySql(sqlConString,
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 4, 12), ServerType.MariaDb))
                    .EnableDetailedErrors();
#endif
            }
        }
    }
}
