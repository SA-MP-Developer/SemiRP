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
        public static List<Phone> GetAllPhone()
        {

            using (var db = new ServerDbContext())
            {
                return db.Phones.ToList();
            }
        }
        public static Phone GetPhoneByNumber(string number)
        {
            using (var db = new ServerDbContext())
            {
                return db.Phones.Select(x => x).Where(w => w.Number == number).FirstOrDefault();
            }
        }
        public static Character GetPhoneOwner(Phone phone)
        {
            using (var db = new ServerDbContext())
            {
                Container container = db.Containers.Select(x => x).Where(w => w == phone.CurrentContainer).FirstOrDefault();
                if(container != null)
                {
                    Character character = db.Characters.Select(x => x).Where(w => w.Inventory == container).FirstOrDefault();
                    return character;
                }
                return null;
            }
        }
        public static void SendSMS(Player sender, string number, string message)
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
            if (!receiver.IsConnected)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] le joueur n'est pas connecté.");
                return;
            }
            sender.SendClientMessage(Color.Yellow, "[SMS] Message envoyé à " + number + " : " + message);
            receiver.SendClientMessage(Color.Yellow, "[SMS] Message reçu de" + phoneSender.Number + " : " + message);
        }
        public static void Call(Player sender, string number)
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
            if (!receiver.IsConnected)
            {
                sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "ERREUR" + Color.White + "] le joueur n'est pas connecté.");
                return;
            }
            sender.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] Appel en cours...");
            receiver.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] Quelqu'un essaie de vous appeler ! /t dec pour décrocher.");
            phoneReceiver.IsRinging = true;
            var timer = new Timer(5000, true);
            int nbr = 0;
            timer.Tick += (sender, e) => {
                if (nbr < 3)
                {
                    receiver.SendClientMessage(Color.White, "[" + Color.DarkRed + "APPEL" + Color.White + "] Quelqu'un essaie de vous appeler ! /t dec pour décrocher.");
                    nbr++;
                }
                else
                {
                    timer.Dispose();
                    nbr = 0;
                    phoneReceiver.IsRinging = false;
                }

            };
        }
    }
}
