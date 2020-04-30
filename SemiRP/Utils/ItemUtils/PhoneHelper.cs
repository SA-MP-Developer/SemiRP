using SampSharp.GameMode;
using SampSharp.GameMode.SAMP;
using SampSharp.Streamer.World;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.PlayerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils.ItemUtils
{
    public class PhoneHelper
    {
        
        public static Phone CreatePhone()
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            Random numberRandom = new Random();
            String number = null;
            do {
                number = numberRandom.Next(10000, 99999).ToString();
            } while (dbContext.Phones.Select(x => x).Where(x => x.Number == number).Any());
            Phone phone = new Phone(number, false, false, null, false,5);
            dbContext.SaveChanges();
            return phone;
        }
        public static void DisplayPhoneNumber(Player player)
        {
            Phone phone = GetDefaultPhone(player.ActiveCharacter);
            if (phone != null)
                Utils.Chat.InfoChat(player, "Votre numéro est : "+GetDefaultPhone(player.ActiveCharacter).Number+".");
            else
                Utils.Chat.InfoChat(player, "Vous n'avez pas de téléphone.");
        }
        public static List<Phone> GetAllPhone()
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            return dbContext.Phones.ToList();
        }
        public static Phone GetPhoneByNumber(string number)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            return dbContext.Phones.Select(x => x).Where(w => w.Number == number).FirstOrDefault();
        }
        public static Character GetPhoneOwner(Phone phone)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            Container container = dbContext.Containers.Select(x => x).Where(w => w == phone.CurrentContainer).FirstOrDefault();
            
                if(container != null)
                {
                    Character character = dbContext.Characters.Select(x => x).Where(w => w.Inventory == container).FirstOrDefault();
                    return character;
                }
                if(container == null)
                {
                    return dbContext.Characters.Where(x => x.ItemInHand == phone).FirstOrDefault();
                }
                return null;
        }
        public static Phone GetDefaultPhone(Character character)
        {
            return GetAllPhone().Select(x => x).Where(x => x.CurrentContainer == character.Inventory).Where(x => x.DefaultPhone == true).FirstOrDefault();
        }
        public static void SetInHandDefaultPhone(Character character)
        {
            if (character.ItemInHand == null)
                throw new Exception("Aucun téléphone en main. Veuillez en prendre un en main.");
            if (!(character.ItemInHand is Phone))
                throw new Exception("L'objet dans la main n'est pas un téléphone.");
            Phone phone = GetDefaultPhone(character);
            if (phone != null)
                phone.DefaultPhone = false;

            ((Phone)character.ItemInHand).DefaultPhone = true;

        }
        public static void SendSMS(Player sender, string number, string message)
        {
            Phone phoneReceiver = Utils.ItemUtils.PhoneHelper.GetPhoneByNumber(number);

            if (phoneReceiver is null)
            {
                throw new Exception("Le numéro " + number + " n'est pas attribué.");
            }
            Character character = Utils.ItemUtils.PhoneHelper.GetPhoneOwner(phoneReceiver);
            Player receiver = PlayerHelper.SearchCharacter(character);
            Phone phoneSender = ContainerHelper.CheckPlayerPhone(sender);
            if (receiver == null || !receiver.IsConnected)
            {
                throw new Exception("Le joueur n'est pas connecté.");
            }
            Chat.SMSChat(sender, "Message envoyé à " + number + " : " + message);
            Chat.SMSChat(receiver, "Message reçu de " + phoneSender.Number + " : " + message);
        }
        public static void Call(Player sender, string number)
        {
            Phone phoneReceiver = PhoneHelper.GetPhoneByNumber(number);
            Object dynamicTextLabelPhone = null;
            if (phoneReceiver is null)
            {
                throw new Exception("le numéro " + number + " n'est pas attribué.");
            }
            if (phoneReceiver.IsRinging || phoneReceiver.IsCalling)
            {
                throw new Exception("le numéro " + number + " est déjà en cours d'appel.");
            }
            Character character = PhoneHelper.GetPhoneOwner(phoneReceiver);
            Player receiver = PlayerHelper.SearchCharacter(character);
            Phone phoneSender = ContainerHelper.CheckPlayerPhone(sender);

            if(phoneReceiver == phoneSender)
            {
                throw new Exception("Vous ne pouvez pas vous appeler vous même.");
            }
            Chat.CallChat(sender, "Appel en cours...");
            
            if(receiver != null) // If a player has the phone
            {
                Chat.CallChat(receiver, "Quelqu'un essaie de vous appeler ("+phoneSender.Number+")! /t dec pour décrocher.");
            }
            else // If the phone is not in a player inventory
            {
                if(phoneReceiver.CurrentContainer == null)
                {
                    Vector3 phoneLabelPosition = new Vector3(phoneReceiver.SpawnLocation.X, phoneReceiver.SpawnLocation.Y, phoneReceiver.SpawnLocation.Z + Constants.Item.PHONE_LABEL_DISTANCE_FROM_PHONE);
                    dynamicTextLabelPhone = Chat.CreateTme("Le téléphone sonne !", phoneLabelPosition, Constants.Item.PHONE_LABEL_DISTANCE);
                }
                
            }
            phoneSender.IsRinging = true;
            phoneSender.PhoneNumberCaller = phoneReceiver.Number;
            phoneReceiver.IsRinging = true;
            phoneReceiver.PhoneNumberCaller = phoneSender.Number;
            var timer = new Timer(5000, true);
            int nbr = 0;
            timer.Tick += (senderPlayer, e) => {
                if(phoneSender.IsCalling && phoneReceiver.IsCalling) // If the players are now in a call
                {
                    timer.Dispose();
                    nbr = 0;
                    try
                    {
                        if (dynamicTextLabelPhone != null)
                        {
                            DynamicTextLabel textLabel = (DynamicTextLabel)dynamicTextLabelPhone;
                            textLabel.Dispose();
                            textLabel = null;
                        }
                    }
                    catch (Exception exception)
                    {

                    }
                }
                else
                {
                    if (nbr < 3)
                    {
                        if (receiver != null) // If a player has the phone
                        {
                            Chat.CallChat(receiver, "Quelqu'un essaie de vous appeler (" + phoneSender.Number + ") ! /t dec pour décrocher.");
                        }
                        else // If the phone is not in a player inventory
                        {

                        }

                        nbr++;
                    }
                    else
                    {
                        timer.Dispose();
                        nbr = 0;
                        Chat.CallChat(sender, "La personne n'a pas décroché.");
                        try
                        {
                            if(dynamicTextLabelPhone != null)
                            {
                                DynamicTextLabel textLabel = (DynamicTextLabel)dynamicTextLabelPhone;
                                textLabel.Dispose();
                                textLabel = null;
                            }
                        }
                        catch(Exception exception)
                        {

                        }
                        phoneSender.IsRinging = false;
                        phoneSender.PhoneNumberCaller = null;
                        phoneReceiver.IsRinging = false;
                        phoneReceiver.PhoneNumberCaller = null;
                    }
                }
                

            };
        }

        public static void PickUp(Player player)
        {
            if (!GetDefaultPhone(player.ActiveCharacter).IsRinging)
            {
                throw new Exception("Vous n'avez aucun appel.");
            }
            Phone phonePlayer = GetDefaultPhone(player.ActiveCharacter);
            Phone phoneCaller = GetPhoneByNumber(phonePlayer.PhoneNumberCaller);
            Character characterCaller = GetPhoneOwner(phoneCaller);
            Player playerCaller = PlayerHelper.SearchCharacter(characterCaller);
            Chat.CallChat(player, "Vous avez décroché.");
            Chat.CallChat(playerCaller, "La personne a décroché.");
            phonePlayer.IsRinging = false;
            phoneCaller.IsRinging = false;
            phonePlayer.IsCalling = true;
            phoneCaller.IsCalling = true;
        }
        public static void HangUp(Player player)
        {
            if (!GetDefaultPhone(player.ActiveCharacter).IsCalling)
            {
                throw new Exception("Vous n'êtes pas en appel.");
            }
            Phone phonePlayer = GetDefaultPhone(player.ActiveCharacter);
            Phone phoneCaller = GetPhoneByNumber(phonePlayer.PhoneNumberCaller);
            Character characterCaller = GetPhoneOwner(phoneCaller);
            Player playerCaller = PlayerHelper.SearchCharacter(characterCaller);
            Chat.CallChat(player, "Vous avez raccroché.");
            Chat.CallChat(playerCaller, "La personne a raccroché.");
            phonePlayer.IsCalling = false;
            phoneCaller.IsCalling = false;
            phonePlayer.PhoneNumberCaller = null;
            phoneCaller.PhoneNumberCaller = null;
            
        }
        public static void DeletePhone(Phone phone)
        {
            if (phone == null)
                return;
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.Phones.Remove(phone);
            dbContext.SaveChanges();
        }
        public static void AddContactToPhoneBook(ContactPhone contactPhone, Phone phone)
        {
            if(phone.MaxContact >= phone.PhoneBook.Count())
            {
                if(phone.PhoneBook.Any(x=>x.Name == contactPhone.Name))
                {
                    throw new Exception("Contact avec le même nom déjà existant.");
                }
                else
                {
                    phone.PhoneBook.Add(contactPhone);
                    ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
                    dbContext.SaveChanges();
                }
            }
        }
        public static void RemoveContactFromPhoneBook(String name, Phone phone)
        {
            if (phone.PhoneBook.RemoveAll(x=>x.Name == name) > 0)
            {
                ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
                dbContext.SaveChanges();
                return;
            }
            else
            {
                throw new Exception("Le contact n'a pas été trouvé.");
            }
        }
        public static ContactPhone CreateContact(String name, String number)
        {
            ContactPhone contactPhone = new ContactPhone(name, number);
            return contactPhone;
        }
        public static ContactPhone GetContactByName(String name, Phone phone)
        {
            ContactPhone contact = phone.PhoneBook.Select(x => x).Where(x => x.Name == name).FirstOrDefault();
            return contact;
        }
        
    }
}
