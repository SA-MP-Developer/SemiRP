using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils.ItemUtils
{
    public class GunHelper
    {
        public static void SaveGunInHand(Player player)
        {
            if (!(player.ActiveCharacter.ItemInHand is Gun))
                throw new Exception("L'objet en main n'est pas une arme");
            player.ActiveCharacter.ItemInHand.Quantity = player.WeaponAmmo;
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
    }
}
