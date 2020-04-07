using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    class Chat
    {
        public static void SendMeChat(Player from, string message)
        {
            foreach (Player p in Player.All)
            {
                if (from.Position.DistanceTo(p.Position) <= Commands.ChatCommands.PROXIMITY_RADIUS)
                {
                    p.SendClientMessage(Color.HotPink, "* " + from.Name + " " + message);
                }
            }
        }

        public static void SendDoChat(Player from, string message)
        {
            foreach (Player p in Player.All)
            {
                if (from.Position.DistanceTo(p.Position) <= Commands.ChatCommands.PROXIMITY_RADIUS)
                {
                    p.SendClientMessage(Color.HotPink, "* (" + from.Name + ") " + message);
                }
            }
        }
    }
}
