﻿using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils.PlayerUtils
{
    public class PlayerHelper
    {
        public static Player SearchCharacter(Character character)
        {
            foreach(Player p in Player.All)
            {
                if(p.ActiveCharacter == character)
                {
                    return p;
                }
            }
            return null;
        }
    }
}