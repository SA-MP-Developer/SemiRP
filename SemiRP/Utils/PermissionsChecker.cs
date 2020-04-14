using Microsoft.EntityFrameworkCore.Internal;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils
{
    class PermissionsChecker
    {
        public static bool IsServerOwner(Player player)
        {
            return player.AccountData.Perms.Any(p => p.Name == "serverowner");
        }

        public static bool IsAdmin(Player player)
        {
            return player.AccountData.Perms.Any(p => p.Name == "admin");
        }

        public static bool IsModerator(Player player)
        {
            return player.AccountData.Perms.Any(p => p.Name == "modo");
        }

        public static bool IsGroupOwner(Player player, Group group)
        {
            foreach (Group g in player.ActiveCharacter.GroupOwner)
            {
                if (g == group)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsGroupMember(Player player, Group group)
        {
            foreach (GroupRank g in player.ActiveCharacter.GroupRanks)
            {
                if (g.ParentGroup == group)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsGroupRank(Player player, GroupRank groupRank)
        {
            foreach (GroupRank g in player.ActiveCharacter.GroupRanks)
            {
                if (g == groupRank)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
