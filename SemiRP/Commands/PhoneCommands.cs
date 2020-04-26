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
            try
            {
                PhoneHelper.SendSMS(sender, number, message);
            }
            catch (Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Le sms n'a pas pu être envoyé à cause d'une erreur : " + e.Message);
            }
        }

        [Command("appel","appeler")]
        private static void Call(Player sender, string number)
        {
            try
            {
                PhoneHelper.Call(sender, number);
            }
            catch (Exception e)
            {
                Utils.Chat.ErrorChat(sender, "L'appel n'a pas pu être démarré à cause d'une erreur : " + e.Message);
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
                Utils.Chat.ErrorChat(sender, "L'appel n'a pas pu être démarré à cause d'une erreur : " + e.Message);
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
                Utils.Chat.ErrorChat(sender, "L'appel n'a pas pu être fini à cause d'une erreur : " + e.Message);
            }
        }
        [Command("numero", "num")]
        private static void Number(Player sender)
        {
            try
            {
                PhoneHelper.DisplayPhoneNumber(sender);
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Le numéro n'a pas pu être affiché à cause d'une erreur : " + e.Message);
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
                Utils.Chat.ErrorChat(sender, "Le téléphone par défaut n'a pas pu être choisi : " + e.Message);
            }

        }

        [CommandGroup("contact", "c", "contacts", "rep","repertoire")]
        public class Contact
        {
            [Command("ajouter", "aj")]
            private static void AddContact(Player sender, String name, String number)
            {
                try
                {
                    Phone phone = PhoneHelper.GetDefaultPhone(sender.ActiveCharacter);
                    ContactPhone contactPhone = PhoneHelper.CreateContact(name, number);
                    PhoneHelper.AddContactToPhoneBook(contactPhone, phone);
                    Utils.Chat.InfoChat(sender, "Le contact " + name + " (" + number + ") a bien été ajouté au répertoire du téléphone par défaut.");

                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le contact n'a pas pu être ajouté dans le répertoire à cause d'une erreur : "+e.Message);
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
                    Utils.Chat.ErrorChat(sender, "Le contact n'a pas pu être supprimé du répertoire à cause d'une erreur : "+e.Message);
                }
                
            }
            [Command("appeler", "appel")]
            private static void CallContact(Player sender, String name)
            {
                try
                {
                    PhoneHelper.Call(sender, PhoneHelper.GetContactByName(name,PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number);
                }
                catch (Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le contact n'a pas pu être appelé à cause d'une erreur : " + e.Message);
                }

            }
            [Command("numero", "num")]
            private static void NumberContact(Player sender, String name)
            {
                try
                {
                    Utils.Chat.InfoChat(sender,"Le numéro du contact est : "+PhoneHelper.GetContactByName(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number);
                }
                catch (Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le numéro du contact n'a pas pu être affiché à cause d'une erreur : " + e.Message);
                }

            }
            [Command("sms")]
            private static void SMSContact(Player sender, String name, String message)
            {
                try
                {
                    PhoneHelper.SendSMS(sender, PhoneHelper.GetContactByName(name, PhoneHelper.GetDefaultPhone(sender.ActiveCharacter)).Number,message);
                }
                catch (Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Le SMS n'a pas pu être envoyé à cause d'une erreur : "+e.Message);
                }

            }
        }
        
    }
}
