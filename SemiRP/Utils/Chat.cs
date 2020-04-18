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
                    float darkenAmount = Math.Clamp(distance / (radius), 0f, Constants.Chat.RANGED_CHAT_FINAL_COLOR_PERC);
                    Color col = baseColor.Darken(darkenAmount);
                    p.SendClientMessage(col, message);
                }
            }
        }

        public static void SendMeChat(Player from, string message)
        {
            var text = "* " + from.Name + " " + message;
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Constants.Chat.ME, text);
            from.SetChatBubble(text, Constants.Chat.ME, Constants.Chat.CHAT_BUBBLE_ME_RANGE, (int)(text.Length / Constants.Chat.CHAT_BUBBLE_TIME_FACTOR));
        }

        public static void SendDoChat(Player from, string message)
        {
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Constants.Chat.ME, "* " + message + " ((" + from.Name + "))");
        }
        public static void ErrorChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.ERROR_TAG + "ERREUR" + Color.White + "] "+message);
        }
        public static void AdminChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.ADMIN_TAG + "ADMIN" + Color.White + "] " + message);
        }
        public static void HelpChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.HELP_TAG + "AIDE" + Color.White + "] " + message);
        }
        public static void CallChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.TEL_TAG + "APPEL" + Color.White + "] " + message);
        }
        public static void SMSChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.SMS_TAG + "SMS" + Color.White + "] " + message);
        }
        public static void ChatInCall(Player player, string denomination, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.TEL_TAG + "APPEL" + Color.White + "] " + denomination + " : " + message);
        }
        public static void ChatInCall(Player player, string denomination, string numero, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.TEL_TAG + "APPEL" + Color.White + "] "+denomination+" (" + numero + ") : " + message);
        }
        public static void InfoChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, "[" + Constants.Chat.INFO_TAG + "INFO" + Color.White + "] " + message);
        }


    }
}
