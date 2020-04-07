using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace SemiRP
{
    public class ServerDbContext : DbContext
    {
        public DbSet<AccountData> Accounts { get; set; }
        public DbSet<CharacterData> Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mydatabase;Trusted_Connection=True;");
#else

#endif
        }
    }

    public class AccountData
    {
        [Key]
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string LastConnectionIP { get; set; }
        public DateTime LastConnectionTime { get; set; }
    }

    public class CharacterData
    {
        [Key]
        public int CharacterId { get; set; }
        public AccountData Account { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
