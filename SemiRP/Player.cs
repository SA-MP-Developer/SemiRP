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

namespace SemiRP
{
    [PooledType]
    public class Player : BasePlayer
    {
        public const int PASSWORD_MAX_ATTEMPTS = 3;

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
                    AccountData = db.Accounts
                        .Single(a => a.Username == this.Name);
            }

            if (userExist)
            {
                PlayerLogin login = new PlayerLogin(this, PASSWORD_MAX_ATTEMPTS);
                login.DialogEnded += (sender, e) =>
                {

                };

                login.Begin();
            }
            else
            {
                PlayerRegistration registration = new PlayerRegistration(this);
                PlayerCharacterCreation charCreation = new PlayerCharacterCreation(this);
                MessageDialog charCreationDialog = new MessageDialog("Inscription", "Vous allez maintenant pouvoir commencer la création de votre personnage.", "Continuer");
                charCreationDialog.Response += (sender, e) =>
                {
                    charCreation.Begin();
                };

                registration.DialogEnded += (sender, e) =>
                {
                    using var db = new ServerDbContext();
                    Account acc = new Account
                    {
                        Email = e.Email,
                        Username = this.Name,
                        Nickname = this.Name,
                        Password = e.Password,
                        LastConnectionIP = this.IP,
                        LastConnectionTime = DateTime.Now
                    };

                    db.Add(acc);
                    db.SaveChanges();

                    this.AccountData = db.Accounts.Single(a => a.Username == this.Name);

                    charCreationDialog.Show(this);
                };

                charCreation.DialogEnded += (sender, e) =>
                {
                    using var db = new ServerDbContext();
                    Character chr = new Character
                    {
                        Account = this.AccountData,
                        Age = e.Age,
                        Name = e.Name,
                        Sex = e.Sex
                    };
                };

                registration.Begin();
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

        #endregion
    }
}