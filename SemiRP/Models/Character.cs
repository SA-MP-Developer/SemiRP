using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Character
    {
        [Key]
        private int characterId;
        private Account account;
        private string name;
        private int age;

        public Character()
        {

        }
        public Character(int characterId, Account account, string name, int age)
        {
            CharacterId = characterId;
            Account = account;
            Name = name;
            Age = age;
        }


        public int CharacterId { get => characterId; set => characterId = value; }
        internal Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
    }
}
