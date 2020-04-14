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

namespace SemiRP
{
    [PooledType]
    public class Player : BasePlayer
    {
        public const int PASSWORD_MAX_ATTEMPTS = 3;
        public const int MAX_CHARACTERS = 2;

        #region Overrides of BasePlayer

        public Account AccountData { get; set; }
        public Character ActiveCharacter { get; set; }

        public bool AcceptMP { get; set; }

        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            bool userExist = false;
            
            using (var db = new ServerDbContext())
            {
                userExist = db.Accounts.Any(a => a.Username == this.Name);
                
                if (userExist)
                    AccountData = db.Accounts.Include(a => a.Perms)
                        .Single(a => a.Username == this.Name);
            }

            this.ToggleSpectating(true);

            if (userExist)
            {
                PlayerLogin login = new PlayerLogin(this, PASSWORD_MAX_ATTEMPTS);
                login.DialogEnded += (sender, e) =>
                {
                    using (var db = new ServerDbContext())
                    {
                        db.Accounts.Attach(this.AccountData);

                        this.AccountData.LastConnectionIP = this.IP;
                        this.AccountData.LastConnectionTime = DateTime.Now;

                        db.SaveChanges();
                    }

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

                    using var db = new ServerDbContext();
                    Account acc = new Account
                    {
                        Email = (string)e.Data["email"],
                        Username = this.Name,
                        Nickname = this.Name,
                        Password = (string)e.Data["password"],
                        LastConnectionIP = this.IP,
                        LastConnectionTime = DateTime.Now
                    };

                    db.Add(acc);
                    db.SaveChanges();

                    this.AccountData = db.Accounts.Single(a => a.Username == this.Name);

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

            foreach (Player p in Player.All)
            {
                float distance = this.Position.DistanceTo(p.Position);
                if (distance < Commands.ChatCommands.PROXIMITY_RADIUS)
                {
                    float darkenAmount = Math.Clamp(distance / Commands.ChatCommands.PROXIMITY_RADIUS, 0f, 0.8f);
                    Color col = Color.White.Darken(darkenAmount);
                    p.SendClientMessage(col, this.Name + " dit : " + e.Text);
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