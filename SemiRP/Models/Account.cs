using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Account
    {
        [Key]
        private int accountId;
        private string username;
        private string password;
        private string email;
        private string nickname;
        private string lastConnectionIP;
        private DateTime lastConnectionTime;


        public Account()
        {
            
        }

        public Account(int accountId, string username, string password, string email, string nickname, string lastConnectionIP, DateTime lastConnectionTime)
        {
            AccountId = accountId;
            Username = username;
            Password = password;
            Email = email;
            Nickname = nickname;
            LastConnectionIP = lastConnectionIP;
            LastConnectionTime = lastConnectionTime;
        }

        
        public int AccountId { get => accountId; set => accountId = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string LastConnectionIP { get => lastConnectionIP; set => lastConnectionIP = value; }
        public DateTime LastConnectionTime { get => lastConnectionTime; set => lastConnectionTime = value; }
    }
}
