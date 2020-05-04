using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace SemiRP.Models
{
    public class Account
    {
        public Account()
        {

        }

        public Account(int id, string username, string password, string email, string nickname, string lastConnectionIP, DateTime lastConnectionTime)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Nickname = nickname;
            LastConnectionIP = lastConnectionIP;
            LastConnectionTime = lastConnectionTime;
        }

        public IList<Permission> GetPerms()
        {
            if (PermsSet.PermissionsSetPermission != null)
                return PermsSet.PermissionsSetPermission.Select(p => p.Permission).ToList();
            return new List<Permission>();
        }

        public bool HavePerm(string name)
        {
            if (PermsSet.PermissionsSetPermission != null)
                return PermsSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == name);
            return false;
        }

        [Key]
        public int Id { get; set; }
        public string Username { get ; set ; }
        public string Password { get ; set ; }
        public string Email { get ; set ; }
        public string Nickname { get ; set ; }
        public string LastConnectionIP { get ; set ; }
        public DateTime LastConnectionTime { get ; set ; }
        [ForeignKey("PermissionSet")]
        public virtual PermissionSet PermsSet { get; set; }
    }
}