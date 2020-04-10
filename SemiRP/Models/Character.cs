using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Character
    {
        public enum CharSex
        {
            NOT_SET = -1,
            MAN = 0,
            WOMAN = 1
        }

        private int id;
        private Account account;
        private string name;
        private uint age;
        private List<Permission> perms;
        private List<GroupRank> groupRanks;
        private List<Group> groupOwner;
        private List<Building> buildingOwner;
        private Container inventory;

        

        public Character()
        {

        }

        public Character(int id, Account account, string name, uint age, List<Permission> perms, List<GroupRank> groupRanks, List<Group> groupOwner, List<Building> buildingOwner, Container inventory)
        {
            this.Id = id;
            this.Account = account;
            this.Name = name;
            this.Age = age;
            this.Perms = perms;
            this.GroupRanks = groupRanks;
            this.GroupOwner = groupOwner;
            this.BuildingOwner = buildingOwner;
            this.Inventory = inventory;
        }
        [Key]
        public int Id { get => id; set => id = value; }
        public Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public uint Age { get => age; set => age = value; }
        public List<Permission> Perms { get => perms; set => perms = value; }
        public List<GroupRank> GroupRanks { get => groupRanks; set => groupRanks = value; }
        public List<Group> GroupOwner { get => groupOwner; set => groupOwner = value; }
        public List<Building> BuildingOwner { get => buildingOwner; set => buildingOwner = value; }
        public Container Inventory { get => inventory; set => inventory = value; }
        public CharSex Sex { get; set; }
    }
}
