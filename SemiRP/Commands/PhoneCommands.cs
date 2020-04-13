using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.PlayerUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("t", "phone", "telephone")]
    class PhoneCommands
    {
        [Command("sms")]
        private static void SendSMS(Player sender, string number, string message)
        {
            Phone phoneReceiver = Utils.ItemUtils.PhoneHelper.GetPhoneByNumber(number);
            
            if (phoneReceiver is null)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] le numéro " + number + " n'est pas attribué.");
                return;
            }
            Character character = Utils.ItemUtils.PhoneHelper.GetPhoneOwner(phoneReceiver);
            Player receiver = PlayerHelper.SearchCharacter(character);
            Phone phoneSender = ContainerHelper.CheckPlayerPhone(sender);
            sender.SendClientMessage(Color.Yellow, "[SMS] Message envoyé à "+number+" : " + message);
            receiver.SendClientMessage(Color.Yellow, "[SMS] Message reçu de" + phoneSender.Number + " : " + message);
        }

        [Command("sms")]
        private static void Call(Player sender, string number)
        {

            
        }
    }
}
