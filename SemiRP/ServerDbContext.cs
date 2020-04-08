using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
#if DEBUG
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["connectionStringDebug"].ConnectionString, b => b.MigrationsAssembly("SemiRP"));
            
#else

#endif
        }
    }
}
