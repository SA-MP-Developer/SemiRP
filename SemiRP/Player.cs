using System;
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

            dbContext = ((GameMode)GameMode.Instance).DbContext;

            bool userExist = false;
            
            userExist = dbContext.Accounts.Any(a => a.Username == this.Name);

            if (userExist)
            {
                AccountData = dbContext.Accounts
                    .Single(a => a.Username == this.Name);
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
                Utils.Chat.ChatInCall(playerCall, phone.Number, e.Text);
                Utils.Chat.ChatInCall(this, phoneCall.Number, e.Text);
                foreach (Player p in Player.All)
                {
                    if(p != this)
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
            this.SetSpawnInfo(NoTeam, 26, this.ActiveCharacter.SpawnLocation.Position, this.ActiveCharacter.SpawnLocation.RotZ);
            this.Name = this.ActiveCharacter.Name;
            this.Spawn();
        }

        #endregion
    }
}