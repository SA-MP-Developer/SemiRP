using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("t", "phone", "telephone")]
    class PhoneCommands
    {
        [Command("sms")]
        private static void SendSMS(Player sender, Player receiver, string message)
        {
            if (!receiver.IsConnected)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] le joueur id " + receiver.Id + " n'est pas connecté.");
                return;
            }

            if (!receiver.AcceptMP)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] " + receiver.Name + " n'accepte pas d'SMS.");
                return;
            }

            sender.SendClientMessage(Color.Yellow, "[SMS] " + sender.Name + " [" + sender.Id + "] : " + message);
            receiver.SendClientMessage(Color.Yellow, "[SMS] " + sender.Name + " [" + sender.Id + "] : " + message);
        }

        [Command("sms")]
        private static void Call(Player sender, string number)
        {

            
        }
    }
}
