using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils;
using SemiRP.Utils.ItemUtils;
using System;

namespace SemiRP.Commands
{
    [CommandGroup("t", "phone", "telephone")]
    public class PhoneCommands
    {
        [Command("sms")]
        private static void SendSMS(Player sender, string number, string message)
        {
            try
            {
                PhoneHelper.SendSMS(sender, number, message);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le sms n'a pas pu être envoyé : " + e.Message);
            }
        }

        [Command("appel", "appeler")]
        private static void Call(Player sender, string number)
        {
            try
            {
                PhoneHelper.Call(sender, number);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "L'appel a échoué : " + e.Message);
            }
        }

        [Command("dec", "decrocher")]
        private static void PickUp(Player sender)
        {
            try
            {
                PhoneHelper.PickUp(sender);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le décrochage a échoué : " + e.Message);
            }
        }

        [Command("rac", "raccrocher")]
        private static void HangUp(Player sender)
        {
            try
            {
                PhoneHelper.HangUp(sender);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le raccrochage a échoué : " + e.Message);
            }
        }

        [Command("numero", "num")]
        private static void Number(Player sender)
        {
            try
            {
                PhoneHelper.DisplayPhoneNumber(sender);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le numéro n'a pas pu être affiché : " + e.Message);
            }
        }

        [Command("defaut", "default")]
        private static void Default(Player sender)
        {
            try
            {
                PhoneHelper.SetInHandDefaultPhone(sender.ActiveCharacter);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le téléphone par défaut n'a pas pu être choisi : " + e.Message);
            }
        }
        [Command("inconnu", "inc")]
        private static void Inconnu(Player sender)
        {
            try
            {
                PhoneHelper.ToggleAnonymPlayerPhone(sender);
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, "Le téléphone n'a pas pu être passé en mode anonyme : " + e.Message);
            }
        }

        [CommandGroup("contact", "c", "contacts", "rep", "repertoire")]
        public class Contact
        {
            [Command("ajouter", "aj", "add")]
            private static void AddContact(Player sender, String name, String number)
            {
                try
                {
                    Phone phone = PhoneHelper.GetDefaultPhone(sender.ActiveCharacter);
                    ContactPhone contactPhone = PhoneHelper.CreateContact(name, number);
                    PhoneHelper.AddContactToPhoneBook(contactPhone, phone);
                    Chat.InfoChat(sender,
                        "Le contact " + name + " (" + number +
                        ") a bien été ajouté au répertoire du téléphone par défaut.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "Le contact n'a pas pu être ajouté dans le répertoire : " + e.Message);
                }
            }

            [Command("retirer", "re", "rem")]
            private static void RemoveContact(Player sender, String name)
            {
                try
                {
                    PhoneHelper.RemoveContactFromPhoneBook(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter));
                    Chat.InfoChat(sender,
                        "Le contact " + name + " a bien été supprimé du répertoire du téléphone par défaut.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "Le contact n'a pas pu être supprimé du répertoire : " + e.Message);
                }
            }

            [Command("appeler", "appel")]
            private static void CallContact(Player sender, String name)
            {
                try
                {
                    PhoneHelper.Call(sender,
                        PhoneHelper.GetContactByName(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number);
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "Le contact n'a pas pu être appelé : " + e.Message);
                }
            }

            [Command("numero", "num")]
            private static void NumberContact(Player sender, String name)
            {
                try
                {
                    Chat.InfoChat(sender,
                        "Le numéro du contact est : " + PhoneHelper
                            .GetContactByName(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number);
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "Le numéro du contact n'a pas pu être affiché : " + e.Message);
                }
            }

            [Command("sms")]
            private static void SMSContact(Player sender, String name, String message)
            {
                try
                {
                    PhoneHelper.SendSMS(sender,
                        PhoneHelper.GetContactByName(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number,
                        message);
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "Le SMS n'a pas pu être envoyé : " + e.Message);
                }
            }
        }
    }
}
