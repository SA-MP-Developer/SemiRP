using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    class Admin
    {
        private static bool CheckAdmin(Player player)
        {
            foreach (Permission p in player.AccountData.Perms)
            {
                if (p.Name == "admin")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
