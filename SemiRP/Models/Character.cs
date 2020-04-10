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
        private List<Permission> perms;
        private List<GroupRank> groupRanks;
        private List<Group> groupOwner;
        private List<Building> buildingOwner;
        private Container inventory;

        public Character()
        {

        }

        public Character(int id, Account account, string name, int age, List<Permission> perms, List<GroupRank> groupRanks, Container inventory)
        {
            this.Id = id;
            this.Account = account;
            this.Name = name;
            this.Age = age;
            this.Perms = perms;
            this.GroupRanks = groupRanks;
            this.Inventory = inventory;
        }

        public int Id { get => id; set => id = value; }
        public Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public List<Permission> Perms { get => perms; set => perms = value; }
        public List<GroupRank> GroupRanks { get => groupRanks; set => groupRanks = value; }
        public Container Inventory { get => inventory; set => inventory = value; }
    }
}
