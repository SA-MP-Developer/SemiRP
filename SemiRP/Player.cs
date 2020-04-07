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

namespace SemiRP
{
    [PooledType]
    partial class Player : BasePlayer
    {
        public const int PASSWORD_MAX_ATTEMPTS = 3;

        #region Overrides of BasePlayer

        public AccountData AccountData { get; set; }
        public CharacterData ActiveCharacter { get; set; }

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
                LogPlayer();
            else
                RegisterPlayer();

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