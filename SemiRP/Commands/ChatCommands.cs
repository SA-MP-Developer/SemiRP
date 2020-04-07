using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    class ChatCommands
    {
        public const float PROXIMITY_RADIUS = 15.0f;
        public const float PROXIMITY_SHOUT_FACTOR = 1.5f;
        public const float PROXIMITY_WHISPER = 5.0f;



        [Command("me")]
        private static void MeCommand(Player sender, string message)
        {
            foreach(Player p in Player.All)
            {
                if (sender.Position.DistanceTo(p.Position) <= PROXIMITY_RADIUS)
                {
                    p.SendClientMessage(Color.HotPink, "* " + sender.Name + " " + message);
                }
            }
        }

        [Command("do")]
        private static void DoCommand(Player sender, string message)
        {
            foreach (Player p in Player.All)
            {
                if (sender.Position.DistanceTo(p.Position) <= PROXIMITY_RADIUS)
                {
                    p.SendClientMessage(Color.HotPink, "* (" + sender.Name + ") " + message);
                }
            }
        }

        [Command("crier", "c", "shout", "s")]
        private static void ShoutCommand(Player sender, string message)
        {
            foreach (Player p in Player.All)
            {
                float distance = sender.Position.DistanceTo(p.Position);
                if (distance <= PROXIMITY_RADIUS * PROXIMITY_SHOUT_FACTOR)
                {
                    float darkenAmount = Math.Clamp(distance / (PROXIMITY_RADIUS * PROXIMITY_SHOUT_FACTOR), 0f, 0.8f);
                    Color col = Color.White.Darken(darkenAmount);
                    p.SendClientMessage(col, sender.Name + " crie : " + message);
                }
            }
        }

        [Command("b", "(")]
        private static void OocCommand(Player sender, string message)
        {
            foreach (Player p in Player.All)
            {
                float distance = sender.Position.DistanceTo(p.Position);
                if (distance <= PROXIMITY_RADIUS * PROXIMITY_SHOUT_FACTOR)
                {
                    float darkenAmount = Math.Clamp(distance / (PROXIMITY_RADIUS * PROXIMITY_SHOUT_FACTOR), 0f, 0.8f);
                    Color col = Color.White.Darken(darkenAmount);
                    p.SendClientMessage(col, "(( " + sender.Name + "[" + sender.Id + "] : " + message + " ))");
                }
            }
        }

        [Command("chuchotter", "chu", "whisper", "wh")]
        private static bool WhisperCommand(Player sender, Player receiver, string message)
        {
            if (sender.Position.DistanceTo(receiver.Position) > PROXIMITY_WHISPER)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] Vous êtes trop loin du joueur ciblé.");
                return false;
            }

            sender.SendClientMessage(Color.White, "Vous chuchottez à " + receiver.Name + " : " + message);
            receiver.SendClientMessage(Color.White, sender.Name + " vous chuchotte : " + message);
            return true;
        }

        [Command("mp", "pm")]
        private static bool PrivateMessageCommand(Player sender, Player receiver, string message)
        {
            if (!receiver.IsConnected)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] le joueur id " + receiver.Id + " n'est pas connecté.");
                return false;
            }

            if (!receiver.AcceptMP)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] " + receiver.Name + " n'accepte pas de MP.");
                return true;
            }

            sender.SendClientMessage(Color.Yellow, "[MP] " + sender.Name + " [" + sender.Id + "] : " + message);
            receiver.SendClientMessage(Color.Yellow, "[MP] " + sender.Name + " [" + sender.Id + "] : " + message);
            return true;
        }
    }
}
