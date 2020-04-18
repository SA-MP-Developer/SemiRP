﻿using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SampSharp.GameMode.Display;
using SemiRP.Utils;
using SampSharp.GameMode.Definitions;
using SemiRP.Models;
using SampSharp.Core.Callbacks;
using SemiRP.PlayerSystems;
using Microsoft.EntityFrameworkCore;
using SemiRP.Models.ItemHeritage;
using System.Text.RegularExpressions;

namespace SemiRP
{
    [PooledType]
    public class Player : BasePlayer
    {
        #region Overrides of BasePlayer

        public Account AccountData { get; set; }
        public Character ActiveCharacter { get; set; }

        public bool AcceptMP { get; set; }

        private ServerDbContext dbContext;

        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            AcceptMP = true;

            dbContext = ((GameMode)GameMode.Instance).DbContext;

            bool userExist = false;
            
            userExist = dbContext.Accounts.Any(a => a.Username == this.Name);

            if (userExist)
            {
                AccountData = dbContext.Accounts
                    .Single(a => a.Username == this.Name);
            }
            else
            {
                Character charactere = dbContext.Characters.Select(x=>x).Where(a => a.Name == this.Name).FirstOrDefault();
                if (charactere !=null)
                {
                    this.Name = charactere.Account.Username;
                    AccountData = dbContext.Accounts
                    .Single(a => a.Username == this.Name);
                    userExist = true;
                }
            }


            this.ToggleSpectating(true);

            if (userExist)
            {
                PlayerLogin login = new PlayerLogin(this, SemiRP.Constants.PASSWORD_MAX_ATTEMPTS);
                login.DialogEnded += (sender, e) =>
                {
                    dbContext.Accounts.Attach(this.AccountData);

                    this.AccountData.LastConnectionIP = this.IP;
                    this.AccountData.LastConnectionTime = DateTime.Now;

                    dbContext.SaveChanges();

                    PlayerCharacterChoice chrChoiceMenu = new PlayerCharacterChoice(this);
                    chrChoiceMenu.Show();
                };

                login.Begin();
            }
            else
            {
                PlayerRegistration regMenu = new PlayerRegistration();

                var regex = new Regex(@"[A-Z][a-z]+_[A-Z][a-z]+([A-Z][a-z]+)*");
                if (regex.IsMatch(this.Name))
                {
                    InputDialog nameChangerDialog = new InputDialog("Changement de nom",
                                               "Bienvenue sur le serveur.\nCe compte n'existe pas, cependant, nous avons détecté que vous utilisez un nom Roleplay.\n" +
                                               "Ce serveur utilise un système de compte avec personnage, vous créerez votre personnage par la suite.\n" +
                                               "Il est donc inutile de garder un nom de compte respectant le format roleplay Prénom_Nom.\n" +
                                               "Nous vous proposons donc de le changer ou non suivant votre souhait. Vous pouvez très bien garder le format Prénom_Nom mais il ne correspondera\n" +
                                               "pas au nom de votre personnage.\n" +
                                               "Veuillez écrire le nom du compte que vous souhaitez utiliser (max 24 charactères) :",
                                               false, "Confirmer", "Retour");
                    nameChangerDialog.Response += (sender, eventArg) =>
                    {
                        if (eventArg.DialogButton == DialogButton.Right)
                        {
                            nameChangerDialog.Show(this);
                            return;
                        }
                        if(eventArg.InputText.Length > 24)
                        {
                            nameChangerDialog.Show(this);
                            return;
                        }
                        if(dbContext.Accounts.Where(x => x.Username == eventArg.InputText).Any() || dbContext.Characters.Where(x => x.Name == eventArg.InputText).Any())
                        {
                            nameChangerDialog.Show(this);
                            return;
                        }
                        this.Name = eventArg.InputText;
                        regMenu.Show(this);


                    };
                    nameChangerDialog.Show(this);
                    return;
                }

                

                regMenu.GetMenu().Ended += (sender, e) =>
                {
                    if (!(e.Data.ContainsKey("password") && e.Data.ContainsKey("email")))
                    {
                        regMenu.Show(this);
                        return;
                    }


                    Account acc = new Account
                    {
                        Email = (string)e.Data["email"],
                        Username = this.Name,
                        Nickname = this.Name,
                        Password = (string)e.Data["password"],
                        LastConnectionIP = this.IP,
                        LastConnectionTime = DateTime.Now,
                        PermsSet = new PermissionSet()
                    };

                    dbContext.Add(acc);
                    dbContext.SaveChanges();

                    this.AccountData = dbContext.Accounts.Single(a => a.Username == this.Name);

                    PlayerCharacterChoice chrChoiceMenu = new PlayerCharacterChoice(this);
                    chrChoiceMenu.Show();
                };

                regMenu.Show(this);
            }
        }


        public override void OnText(TextEventArgs e)
        {
            base.OnText(e);
            e.SendToPlayers = false;
            var defaultPhone = Utils.ItemUtils.PhoneHelper.GetDefaultPhone(this.ActiveCharacter);
            if (defaultPhone != null && defaultPhone.IsCalling)
            {
                Phone phone = Utils.ItemUtils.PhoneHelper.GetDefaultPhone(this.ActiveCharacter);
                Phone phoneCall = Utils.ItemUtils.PhoneHelper.GetPhoneByNumber(phone.PhoneNumberCaller);
                Character characterCall = Utils.ItemUtils.PhoneHelper.GetPhoneOwner(phoneCall);
                Player playerCall = Utils.PlayerUtils.PlayerHelper.SearchCharacter(characterCall);
                Utils.Chat.ChatInCall(playerCall,"Vous", phone.Number, e.Text);
                Utils.Chat.ChatInCall(this,"La personne", phoneCall.Number, e.Text);
                foreach (Player p in Player.All)
                {
                    if(p != this && p != playerCall)
                    {
                        float distance = this.Position.DistanceTo(p.Position);
                        if (distance < SemiRP.Constants.PROXIMITY_RADIUS)
                        {
                            float darkenAmount = Math.Clamp(distance / SemiRP.Constants.PROXIMITY_RADIUS, 0f, 0.8f);
                            Color col = Color.White.Darken(darkenAmount);
                            p.SendClientMessage(col, this.Name + " dit (Tél.) : " + e.Text);
                        }
                    }
                }
            }
            else
            {
                foreach (Player p in Player.All)
                {
                    float distance = this.Position.DistanceTo(p.Position);
                    if (distance < SemiRP.Constants.PROXIMITY_RADIUS)
                    {
                        float darkenAmount = Math.Clamp(distance / SemiRP.Constants.PROXIMITY_RADIUS, 0f, 0.8f);
                        Color col = Color.White.Darken(darkenAmount);
                        p.SendClientMessage(col, this.Name + " dit : " + e.Text);
                    }
                }
            }
        }

        public void SpawnCharacter()
        {
            if (this.ActiveCharacter == null)
                return;

            this.ToggleSpectating(false);
            this.SetSpawnInfo(NoTeam, (int)this.ActiveCharacter.Skin, this.ActiveCharacter.SpawnLocation.Position, this.ActiveCharacter.SpawnLocation.RotZ);
            this.Name = this.ActiveCharacter.Name;
            this.Spawn();
        }

        public override void OnSpawned(SpawnEventArgs e)
        {
            base.OnSpawned(e);

            this.Skin = (int)this.ActiveCharacter.Skin;
        }

        #endregion
    }
}