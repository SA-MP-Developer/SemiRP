using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Utils;

namespace SemiRP.Commands
{
    class ChatCommands
    {
        [Command("me")]
        private static void MeCommand(Player sender, string message)
        {
            Chat.SendMeChat(sender, message);
        }

        [Command("do")]
        private static void DoCommand(Player sender, string message)
        {
            Chat.SendDoChat(sender, message);
        }

        [Command("crier", "c", "shout", "s")]
        private static void ShoutCommand(Player sender, string message)
        {
            Chat.SendGradientRangedChat(sender, Constants.PROXIMITY_RADIUS * Constants.PROXIMITY_SHOUT_FACTOR, Color.White, sender.Name + " crie : " + message + " !");
        }
        
        [Command("vb", "vbasse")]
        private static void LowVoiceCommand(Player sender, string message)
        {
            Chat.SendGradientRangedChat(sender, Constants.PROXIMITY_RADIUS * Constants.PROXIMITY_LOW_VOICE_FACTOR, Color.White, sender.Name + " [voix basse] : " + message);
        }

        [Command("b", "(")]
        private static void OocCommand(Player sender, string message)
        {
            Chat.SendGradientRangedChat(sender, Constants.PROXIMITY_RADIUS, Color.White, "(( " + sender.Name + "[" + sender.Id + "] : " + message + " ))");
        }

        [Command("chuchotter", "chu", "whisper", "wh", "w")]
        private static void WhisperCommand(Player sender, Player receiver, string message)
        {
            if (sender.Position.DistanceTo(receiver.Position) > Constants.PROXIMITY_WHISPER)
            {
                Chat.ErrorChat(sender, "Vous êtes trop loin du joueur ciblé.");
                return;
            }

            sender.SendClientMessage(Color.YellowGreen, "Chuchottement à " + receiver.Name + " : " + Color.White + message);
            receiver.SendClientMessage(Color.YellowGreen, sender.Name + " vous chuchotte : " + Color.White + message);
        }

        [Command("mp", "pm")]
        private static void PrivateMessageCommand(Player sender, Player receiver, string message)
        {
            if (!receiver.IsConnected)
            {
                Chat.ErrorChat(sender, "Le joueur id " + receiver.Id + " n'est pas connecté.");
                return;
            }

            if (!receiver.AcceptMP)
            {
                Chat.ErrorChat(sender, receiver.Name + " n'accepte pas de MP.");
                return;
            }

            sender.SendClientMessage(Constants.Chat.PM, "[MP] Envoyé à " + sender.Name + "(" + sender.Id + ") : " + message);
            receiver.SendClientMessage(Constants.Chat.PM, "[MP] Reçu de " + sender.Name + "(" + sender.Id + ") : " + message);
        }

        [Command("activermp", "activerpm", "togglemp"," togglepm")]
        private static void TogglePm(Player sender)
        {
            if (sender.AcceptMP)
            {
                sender.AcceptMP = false;
                Chat.ClientChat(sender, "Vous avez " + Constants.Chat.DISABLED + "désactivé" + Color.White + " vos message privés (mp).");
            }
            else
            {
                sender.AcceptMP = true;
                Chat.ClientChat(sender, "Vous avez " + Constants.Chat.SUCCESS + "activé" + Color.White + " vos message privés (mp).");
            }
        }
    }
}
