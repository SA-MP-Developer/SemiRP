using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("a", "admin")]
    class AdminCommands
    {
        [Command("help", "h")]
        private static void Help(Player sender)
        {
            Utils.Chat.AdminChat(sender, "help");
        }
    }
}
