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
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Color.HotPink, "* " + from.Name + " " + message);
        }

        public static void SendDoChat(Player from, string message)
        {
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Color.HotPink, "* (" + from.Name + ") " + message);
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
        public static void CallChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] " + message);
        }
        public static void SMSChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.OrangeRed + "SMS" + Color.White + "] " + message);
        }
        public static void ChatInCall(Player player, string numero, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] (" + numero + ") : " + message);
        }
        public static void ChatInCall(Player player, string denomination, string numero, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] "+denomination+" (" + numero + ") : " + message);
        }
        public static void InfoChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Color.DarkRed + "INFO" + Color.White + "] " + message);
        }


    }
}
