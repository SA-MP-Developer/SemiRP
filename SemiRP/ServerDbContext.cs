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

namespace SemiRP
{
    public class ServerDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["connectionStringDebug"].ConnectionString,
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
