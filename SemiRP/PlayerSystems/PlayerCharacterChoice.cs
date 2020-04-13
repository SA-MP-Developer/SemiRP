using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SemiRP.PlayerSystems
{
    class PlayerCharacterChoice
    {

        ListDialog charList;

        List<Character> playerChars;

        PlayerCharacterCreation charCreationMenu;

        Player player;

        public PlayerCharacterChoice(Player player)
        {
            this.player = player;

            using (var db = new ServerDbContext())
            {
                playerChars = db.Characters.Select(c => c).Where(c => c.Account == player.AccountData).ToList();
            }

            if (playerChars.Count == 0)
            {
                charCreationMenu = new PlayerCharacterCreation();
            }
            else
            {
                charList = new ListDialog("Choix de personnage", "Confirmer", "Quitter");

                foreach (Character chr in playerChars)
                {
                    charList.AddItem(chr.Name + " (" + Utils.SexUtils.SexToString(chr.Sex) + ", " + chr.Age + " ans)");
                }
            }
        }

        public void Show()
        {
            if (charCreationMenu != null)
            {
                charCreationMenu.GetMenu().Ended += (sender, e) =>
                {
                    if (!(e.Data.ContainsKey("name") && e.Data.ContainsKey("age") && e.Data.ContainsKey("sex")))
                    {
                        charCreationMenu.Show(player);
                        return;
                    }

                    using (var db = new ServerDbContext())
                    {
                        db.Accounts.Attach(player.AccountData);

                        Character chr = new Character();
                        chr.Account = player.AccountData;
                        chr.Name = (string)e.Data["name"];
                        chr.Age = (uint)e.Data["age"];
                        chr.Sex = (Character.CharSex)e.Data["sex"];

                        db.Add(chr);
                        db.SaveChanges();
                    }
                };
                charCreationMenu.Show(player);
                return;
            }

            charList.Show(player);
        }

        public void ChoiceRespons(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                e.Player.Kick();
                return;
            }

            if (e.ListItem > playerChars.Count)
            {
                player.ActiveCharacter = playerChars[e.ListItem];
                player.SendClientMessage("Tu as choisi " + player.ActiveCharacter.Name);
            }
        }
    }
}
