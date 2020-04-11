using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    class Chat
    {
        public static void SendRangedChat(Player from, float radius, Color color, string message)
        {
            foreach (Player p in Player.All)
            {
                if (from.Position.DistanceTo(p.Position) <= radius)
                {
                    p.SendClientMessage(color, message);
                }
            }
        }

        public static void SendRangedChat(Player from, float radius, string message)
        {
            SendRangedChat(from, radius, Color.White, message);
        }

        public static void SendGradientRangedChat(Player from, float radius, Color baseColor, string message)
        {
            foreach (Player p in Player.All)
            {
                float distance = from.Position.DistanceTo(p.Position);
                if (distance <= radius)
                {
                    float darkenAmount = Math.Clamp(distance / (radius), 0f, 0.8f);
                    Color col = baseColor.Darken(darkenAmount);
                    p.SendClientMessage(col, message);
                }
            }
        }

        public static void SendMeChat(Player from, string message)
        {
            SendRangedChat(from, Commands.ChatCommands.PROXIMITY_RADIUS, Color.HotPink, "* " + from.Name + " " + message);
        }

        public static void SendDoChat(Player from, string message)
        {
            SendRangedChat(from, Commands.ChatCommands.PROXIMITY_RADIUS, Color.HotPink, "* (" + from.Name + ") " + message);
        }
        public static void ErrorChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] "+message);
        }
        public static void AdminChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.Red + "ADMIN" + Color.White + "] " + message);
        }
        public static void HelpChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DeepSkyBlue + "AIDE" + Color.White + "] " + message);
        }
    }
}
