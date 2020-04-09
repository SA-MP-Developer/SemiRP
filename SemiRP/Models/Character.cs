using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Character
    {
        private int id;
        private Account account;
        private string name;
        private int age;

        public Character()
        {

        }
        public Character(int id, Account account, string name, int age)
        {
            Id = id;
            Account = account;
            Name = name;
            Age = age;
        }


        [Key]
        public int Id { get => id; set => id = value; }
        internal Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
    }
}
