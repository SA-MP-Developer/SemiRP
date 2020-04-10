﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class GroupRank
    {
        private int id;
        private String name;
        private Group group;

        public GroupRank(int id, string name, Group group)
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal Group Group { get => group; set => group = value; }
    }
}
