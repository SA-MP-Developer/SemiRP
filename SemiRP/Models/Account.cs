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
        private int id;
        private string username;
        private string password;
        private string email;
        private string nickname;
        private string lastConnectionIP;
        private DateTime lastConnectionTime;


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
        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string LastConnectionIP { get => lastConnectionIP; set => lastConnectionIP = value; }
        public DateTime LastConnectionTime { get => lastConnectionTime; set => lastConnectionTime = value; }
        [ForeignKey("PermissionSet")]
        public PermissionSet PermsSet { get; set; }
    }
}