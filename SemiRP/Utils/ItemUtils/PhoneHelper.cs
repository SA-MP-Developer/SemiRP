using SampSharp.GameMode.SAMP;
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
            Phone phone = new Phone(number, false, false, null, false);
            dbContext.SaveChanges();
            return phone;
        }
        public static void DisplayPhoneNumber(Player player)
        {
            Phone phone = GetDefaultPhone(player.ActiveCharacter);
            if (phone != null)
                Utils.Chat.CallChat(player, "Votre numéro est : "+GetDefaultPhone(player.ActiveCharacter).Number+".");
            else
                Utils.Chat.CallChat(player, "Vous n'avez pas de téléphone.");
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
                return null;
        }
        public static Phone GetDefaultPhone(Character character)
        {
            return GetAllPhone().Select(x => x).Where(x => x.CurrentContainer == character.Inventory).Where(x => x.DefaultPhone == true).FirstOrDefault();
        }
        public static void SendSMS(Player sender, string number, string message)
        {
            Phone phoneReceiver = Utils.ItemUtils.PhoneHelper.GetPhoneByNumber(number);

            if (phoneReceiver is null)
            {
                Chat.ErrorChat(sender, "Le numéro " + number + " n'est pas attribué.");
                return;
            }
            Character character = Utils.ItemUtils.PhoneHelper.GetPhoneOwner(phoneReceiver);
            Player receiver = PlayerHelper.SearchCharacter(character);
            Phone phoneSender = ContainerHelper.CheckPlayerPhone(sender);
            if (receiver == null || !receiver.IsConnected)
            {
                Chat.ErrorChat(sender, "Le joueur n'est pas connecté.");
                return;
            }
            Chat.SMSChat(sender, "Message envoyé à " + number + " : " + message);
            Chat.SMSChat(receiver, "Message reçu de " + phoneSender.Number + " : " + message);
        }
        public static void Call(Player sender, string number)
        {
            Phone phoneReceiver = Utils.ItemUtils.PhoneHelper.GetPhoneByNumber(number);
            if (phoneReceiver is null)
            {
                Chat.ErrorChat(sender, "le numéro " + number + " n'est pas attribué.");
                return;
            }
            if (phoneReceiver.IsRinging || phoneReceiver.IsCalling)
            {
                Chat.ErrorChat(sender, "le numéro " + number + " est déjà en cours d'appel.");
            }
            Character character = Utils.ItemUtils.PhoneHelper.GetPhoneOwner(phoneReceiver);
            Player receiver = PlayerHelper.SearchCharacter(character);
            Phone phoneSender = ContainerHelper.CheckPlayerPhone(sender);

            if (!receiver.IsConnected)
            {
                Chat.ErrorChat(sender, "Le joueur n'est pas connecté.");
                return;
            }
            if(phoneReceiver == phoneSender)
            {
                Chat.ErrorChat(sender, "Vous ne pouvez pas vous appeler vous même.");
                return;
            }
            Chat.CallChat(sender, "Appel en cours...");
            
            if(receiver != null) // If a player has the phone
            {
                Chat.CallChat(receiver, "Quelqu'un essaie de vous appeler ! /t dec pour décrocher.");
            }
            else // If the phone is not in a player inventory
            {

            }
            phoneSender.IsRinging = true;
            phoneSender.PhoneNumberCaller = phoneReceiver.Number;
            phoneReceiver.IsRinging = true;
            phoneReceiver.PhoneNumberCaller = phoneSender.Number;
            var timer = new Timer(5000, true);
            int nbr = 0;
            timer.Tick += (sender, e) => {
                if(phoneSender.IsCalling && phoneReceiver.IsCalling) // If the players are now in a call
                {
                    timer.Dispose();
                    nbr = 0;
                }
                else
                {
                    if (nbr < 3)
                    {
                        if (receiver != null) // If a player has the phone
                        {
                            Chat.CallChat(receiver, "Quelqu'un essaie de vous appeler ! /t dec pour décrocher.");
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
                Chat.ErrorChat(player, "Vous n'avez aucun appel.");
                return;
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
                Chat.ErrorChat(player, "Vous n'êtes pas en appel.");
                return;
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
    }
}
