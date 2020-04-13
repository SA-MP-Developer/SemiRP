using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models.ContainerHeritage
{
    public class Inventory : Container
    {
        private Character character;

        public Inventory(Character character)
        {
            this.Character = character;
        }

        public Character Character { get => character; set => character = value; }
    }
}
