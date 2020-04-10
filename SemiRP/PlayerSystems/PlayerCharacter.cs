using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.PlayerSystems
{
    public class CharCreationDialogEndEventArgs : EventArgs
    {

    }

    class PlayerCharacterCreation
    {
        private readonly Player player;

        public PlayerCharacterCreation(Player player)
        {
            this.player = player;
        }

        protected virtual void OnDialogEnded(CharCreationDialogEndEventArgs e)
        {
            DialogEnded?.Invoke(this, e);
        }

        public event EventHandler<CharCreationDialogEndEventArgs> DialogEnded;
    }
}
