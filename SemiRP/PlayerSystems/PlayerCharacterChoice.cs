﻿using Microsoft.EntityFrameworkCore;
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
    public class PlayerCharacterChoice
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
                playerChars = db.Characters.Select(c => c).Where(c => c.Account == player.AccountData)
                    .Include(s => s.SpawnLocation)
                    .Include(c => c.PermsSet).ThenInclude(p => p.PermissionsSetPermission).ThenInclude(p => p.Permission)
                    .Include(c => c.GroupRanks)
                    .Include(c => c.GroupOwner)
                    .Include(c => c.BuildingOwner)
                    .Include(c => c.Inventory)
                    .ToList();
            }

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
                    charList.AddItem(chr.Name + " (" + Utils.SexUtils.SexToString(chr.Sex) + ", " + chr.Age + " ans)");
                }

                if (playerChars.Count < SemiRP.Constants.MAX_CHARACTERS)
                {
                    charList.AddItem("Créer un autre personnage...");
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

                        SpawnLocation chrSpawn = new SpawnLocation();
                        chrSpawn.Interior = 0;
                        chrSpawn.VirtualWorld = 0;
                        chrSpawn.X = 1762.1357f;
                        chrSpawn.Y = -1862.8958f;
                        chrSpawn.Z = 13.5757f;
                        chrSpawn.RotX = 0f;
                        chrSpawn.RotY = 0f;
                        chrSpawn.RotZ = 269.4686f;

                        Character chr = new Character();
                        chr.Account = player.AccountData;
                        chr.Name = (string)e.Data["name"];
                        chr.Age = (uint)e.Data["age"];
                        chr.Sex = (Character.CharSex)e.Data["sex"];
                        chr.SpawnLocation = chrSpawn;

                        player.ActiveCharacter = chr;

                        db.Add(chr);
                        db.SaveChanges();
                    }

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
