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
    public class PhoneCommands
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
        [Command("numero", "num")]
        private static void Number(Player sender)
        {
            PhoneHelper.DisplayPhoneNumber(sender);
        }

        [CommandGroup("contact", "c")]
        public class Contact
        {
            [Command("ajouter", "aj")]
            private static void AddContact(Player sender, String name, String number)
            {
                try
                {
                    PhoneHelper.AddContactToPhoneBook(PhoneHelper.CreateContact(name, number), PhoneHelper.GetDefaultPhone(sender.ActiveCharacter));
                    Utils.Chat.InfoChat(sender, "Le contact " + name + " (" + number + ") a bien été ajouté au répertoire du téléphone par défaut.");

                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le contact n'a pas pu être ajouté dans le répertoire à cause d'une erreur.");
                }
            }
            [Command("retirer", "re")]
            private static void RemoveContact(Player sender, String name)
            {
                try
                {
                    PhoneHelper.RemoveContactFromPhoneBook(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter));
                    Utils.Chat.InfoChat(sender, "Le contact " + name + " a bien été supprimé du répertoire du téléphone par défaut.");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le contact n'a pas pu être supprimé du répertoire à cause d'une erreur.");
                }
                
            }
        }
        
    }
}
