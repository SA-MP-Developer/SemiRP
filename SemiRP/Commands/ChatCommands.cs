using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    class ChatCommands
    {
        [Command("me")]
        private static void MeCommand(Player sender, string message)
        {
            Utils.Chat.SendMeChat(sender, message);
        }

        [Command("do")]
        private static void DoCommand(Player sender, string message)
        {
            Utils.Chat.SendDoChat(sender, message);
        }

        [Command("crier", "c", "shout", "s")]
        private static void ShoutCommand(Player sender, string message)
        {
            Utils.Chat.SendGradientRangedChat(sender, SemiRP.Constants.PROXIMITY_RADIUS * SemiRP.Constants.PROXIMITY_SHOUT_FACTOR, Color.White, sender.Name + " crie : " + message);
        }

        [Command("b", "(")]
        private static void OocCommand(Player sender, string message)
        {
            Utils.Chat.SendGradientRangedChat(sender, SemiRP.Constants.PROXIMITY_RADIUS, Color.White, "(( " + sender.Name + "[" + sender.Id + "] : " + message + " ))");
        }

        [Command("chuchotter", "chu", "whisper", "wh", "w")]
        private static void WhisperCommand(Player sender, Player receiver, string message)
        {
            if (sender.Position.DistanceTo(receiver.Position) > SemiRP.Constants.PROXIMITY_WHISPER)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] Vous êtes trop loin du joueur ciblé.");
                return;
            }

            sender.SendClientMessage(Color.White, "Vous chuchottez à " + receiver.Name + " : " + message);
            receiver.SendClientMessage(Color.White, sender.Name + " vous chuchotte : " + message);
        }

        [Command("mp", "pm")]
        private static void PrivateMessageCommand(Player sender, Player receiver, string message)
        {
            if (!receiver.IsConnected)
            {
                Utils.Chat.ErrorChat(sender, "le joueur id " + receiver.Id + " n'est pas connecté.");
                return;
            }

            if (!receiver.AcceptMP)
            {
                Utils.Chat.ErrorChat(sender, receiver.Name + " n'accepte pas de MP.");
                return;
            }

            sender.SendClientMessage(Constants.Chat.PM, "[MP] " + sender.Name + " [" + sender.Id + "] : " + message);
            receiver.SendClientMessage(Constants.Chat.PM, "[MP] " + sender.Name + " [" + sender.Id + "] : " + message);
        }

        [Command("activermp", "activerpm", "togglemp"," togglepm")]
        private static void TogglePm(Player sender)
        {
            if (sender.AcceptMP)
            {
                sender.AcceptMP = false;
                Utils.Chat.InfoChat(sender, "Vous avez " + Constants.Chat.HIGHLIGHT + "désactivé" + Color.White + " vos message privés (mp).");
            }
            else
            {
                sender.AcceptMP = true;
                Utils.Chat.InfoChat(sender, "Vous avez " + Constants.Chat.HIGHLIGHT + "activé" + Color.White + " vos message privés (mp).");
            }
        }
    }
}
