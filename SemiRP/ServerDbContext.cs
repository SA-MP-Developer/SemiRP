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
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

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
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["connectionStringRelease"].ConnectionString,
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 4, 12), ServerType.MariaDb))
                    .EnableDetailedErrors();
#endif
            }
        }
    }
}
