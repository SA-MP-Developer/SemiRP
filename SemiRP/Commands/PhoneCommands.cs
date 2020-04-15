using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.ItemUtils;
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
            PhoneHelper.SendSMS(sender, number, message);
        }

        [Command("appel","appeler")]
        private static void Call(Player sender, string number)
        {
            PhoneHelper.Call(sender, number);
        }

        [Command("dec", "decrocher")]
        private static void PickUp(Player sender)
        {
            PhoneHelper.PickUp(sender);
        }

        [Command("rac", "raccrocher")]
        private static void HangUp(Player sender)
        {
            PhoneHelper.HangUp(sender);
        }
    }
}
