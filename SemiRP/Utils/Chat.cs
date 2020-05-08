using SampSharp.GameMode;
using SampSharp.GameMode.SAMP;
using SampSharp.Streamer.World;
using System;

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
        
        public static void ClientChat(Player player, string message)
        {
            player.SendClientMessage(Color.White, message);
        }

        public static void SendMeChat(Player from, string message)
        {
            var text = from.Name + " " + message;
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Constants.Chat.ME, text);
            from.SetChatBubble(text, Constants.Chat.ME, Constants.Chat.CHAT_BUBBLE_ME_RANGE, (int)(text.Length / Constants.Chat.CHAT_BUBBLE_TIME_FACTOR));
        }

        public static void SendDoChat(Player from, string message)
        {
            SendRangedChat(from, SemiRP.Constants.PROXIMITY_RADIUS, Constants.Chat.ME, message + " ((" + from.Name + "))");
        }

        public static void ErrorChat(Player player, string message)
        {
            player.SendClientMessage(Constants.Chat.ERROR, message);
        }

        public static void AdminChat(Player player, string message)
        {
            player.SendClientMessage(Constants.Chat.ADMIN, "[ADMIN] " + Color.White + message);
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
            player.SendClientMessage(Constants.Chat.INFO, "[INFO] " + Color.White + message);
        }

        public static void SendToAllAdmin(Player player, string message)
        {
            foreach (Player p in Player.All)
            {
                if (p.AccountData.HavePerm("admin"))
                {
                    p.SendClientMessage(Color.White, "[" + Constants.Chat.INFO + "INFO" + Color.White + "] " + "[JOUEUR:"+player.ActiveCharacter.Name+"] "+message);
                }
            }
        }

        public static DynamicTextLabel CreateTme(String message,Vector3 position, float drawdistance,Player attachedPlayer = null,Vehicle attachedVehicle = null,bool testLOS = false,int worldid =-1,int interiorid =-1, Player player = null, float streamdistance = 100, DynamicArea area = null, int priority = 0)
        {
             return new DynamicTextLabel(message, Constants.Chat.ME, position,drawdistance,attachedPlayer,attachedVehicle,testLOS,worldid,interiorid,player,streamdistance,area,priority);
        }
        public static DynamicTextLabel CreateTmeTimer(String message, Vector3 position, float drawdistance, int time, Player attachedPlayer = null, Vehicle attachedVehicle = null, bool testLOS = false, int worldid = -1, int interiorid = -1, Player player = null, float streamdistance = 100, DynamicArea area = null, int priority = 0)
        {
            DynamicTextLabel dynamicTextLabel = new DynamicTextLabel(message, Constants.Chat.ME, position, drawdistance, attachedPlayer, attachedVehicle, testLOS, worldid, interiorid, player, streamdistance, area, priority);
            var timerKick = new Timer(time, false);
            timerKick.Tick += (senderPlayer, e) =>
            {
                dynamicTextLabel.Dispose();
            };
            return dynamicTextLabel;
        }


    }
}
