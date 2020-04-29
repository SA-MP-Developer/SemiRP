using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils.PlayerUtils
{
    public class PlayerHelper
    {
        public static Player SearchCharacter(Character character)
        {
            if(character is null)
            {
                return null;
            }
            foreach(Player p in Player.All)
            {
                if(p.ActiveCharacter == character)
                {
                    return p;
                }
            }
            return null;
        }

        public static string HealthToDescription(Player player)
        {
            if (player.Health > 95.0f)
            {
                return "* En parfaite santé";
            }
            else if (player.Health > 80.0f)
            {
                return "* Quelques égratignures";
            }
            else if (player.Health > 50.0f)
            {
                return "* Des blessures importantes";
            }
            else if (player.Health > 25.0f)
            {
                return "* Très mal en point";
            }
            else if (player.Health > 0.0f)
            {
                return "* Proche de la mort";
            }
            return "* En parfaite santé";
        }
    }
}
