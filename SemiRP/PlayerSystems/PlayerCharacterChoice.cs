using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;
using SemiRP.Models.ContainerHeritage;
using System.Collections.Generic;
using System.Linq;

namespace SemiRP.PlayerSystems
{
    public class PlayerCharacterChoice
    {

        ListDialog charList;

        List<Character> playerChars;

        PlayerCharacterCreation charCreationMenu;

        Player player;

        public PlayerCharacterChoice(Player player)
        {
            this.player = player;

            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            playerChars = dbContext.Characters.Select(c => c).Where(c => c.Account == player.AccountData).ToList();

            if (playerChars.Count == 0)
            {
                charCreationMenu = new PlayerCharacterCreation();
            }
            else
            {
                charList = new ListDialog("Choix de personnage", "Confirmer", "Quitter");
                charList.Response += ChoiceResponse;

                foreach (Character chr in playerChars)
                {
                    charList.AddItem(chr.Name + " (Niveau " + chr.Level + " - " + Utils.SexUtils.SexToString(chr.Sex) + " de " + chr.Age + " ans)");
                }

                if (playerChars.Count < SemiRP.Constants.MAX_CHARACTERS)
                {
                    charList.AddItem(Color.DarkGray + "Créer un autre personnage...");
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

                    ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

                    dbContext.Accounts.Attach(player.AccountData);

                    SpawnLocation chrSpawn = new SpawnLocation();
                    chrSpawn.Interior = 0;
                    chrSpawn.VirtualWorld = 0;
                    chrSpawn.X = 1762.1357f;
                    chrSpawn.Y = -1862.8958f;
                    chrSpawn.Z = 13.5757f;
                    chrSpawn.RotX = 0f;
                    chrSpawn.RotY = 0f;
                    chrSpawn.RotZ = 269.4686f;

                    Inventory inv = new Inventory();
                    inv.MaxSpace = Constants.CHARACTER_INVENTORY_SIZE;

                    Character chr = new Character();
                    chr.Account = player.AccountData;
                    chr.Name = (string)e.Data["name"];
                    chr.Age = (uint)e.Data["age"];
                    chr.Sex = (Character.CharSex)e.Data["sex"];
                    chr.Skin = 26;
                    chr.SpawnLocation = chrSpawn;
                    chr.Inventory = inv;
                    chr.PermsSet = new PermissionSet();
                    chr.GroupOwner = new List<Group>();
                    chr.GroupRanks = new List<GroupRank>();
                    chr.BuildingOwner = new List<Building>();

                    player.ActiveCharacter = chr;

                    dbContext.Add(chr);
                    dbContext.SaveChanges();

                    player.SpawnCharacter();
                };
                charCreationMenu.Show(player);
                return;
            }

            charList.Show(player);
        }

        public void ChoiceResponse(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                e.Player.Kick();
                return;
            }

            if (e.ListItem < playerChars.Count)
            {
                player.ActiveCharacter = playerChars[e.ListItem];
                player.SendClientMessage("Tu as choisi " + player.ActiveCharacter.Name);
                player.SpawnCharacter();
            }
            else
            {
                charCreationMenu = new PlayerCharacterCreation();
                this.Show();
            }
        }
    }
}
