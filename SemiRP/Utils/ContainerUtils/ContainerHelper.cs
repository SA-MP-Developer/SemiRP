using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils.ContainerUtils
{
    public class ContainerHelper
    {
        public static Phone CheckPlayerPhone(Player player)
        {
            return player.ActiveCharacter.Inventory.ListItems.Select(x => x).OfType<Phone>().Where(w => w.DefaultPhone).FirstOrDefault();
        }
    }
}
