using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    public class MenuDialogItemEventArgs
    {
        public MenuDialog Parent;
        public Hashtable ParentData;
        public BasePlayer Player;
    }

    public class MenuDialogItem
    {
        public event EventHandler<MenuDialogItemEventArgs> Selected;

        public string Name { get; set; }

        public MenuDialogItem(string name)
        {
            this.Name = name;
        }

        public virtual void OnSelected(MenuDialogItemEventArgs e)
        {
            Selected?.Invoke(this, e);
        }
    }

    public class MenuDialogEventArgs
    {
        public Hashtable Data;
        public DialogButton Button;
    }

    public class MenuDialog
    {
        private List<MenuDialogItem> menuItems;

        private ListDialog menuDialog;

        public event EventHandler<MenuDialogEventArgs> Ended;

        private Hashtable itemsData;

        private MenuDialogItem continueItem;

        public MenuDialog(string caption, string button1, string button2 = null, string continueName = null)
        {
            menuDialog = new ListDialog(caption, button1, button2);
            menuDialog.Response += MenuResponse;

            menuItems = new List<MenuDialogItem>();

            itemsData = new Hashtable();

            continueItem = null;

            if (continueName != null)
            {
                continueItem = new MenuDialogItem(continueName);
                continueItem.Selected += ContinueResponse;
            }
        }

        public virtual void OnEnded(MenuDialogEventArgs e)
        {
            Ended?.Invoke(this, e);
        }

        public void AddItem(MenuDialogItem item)
        {
            menuItems.Add(item);
        }

        public void Show(BasePlayer player)
        {
            menuDialog.Items.Clear();

            if (continueItem != null)
            {
                if (menuItems.Contains(continueItem))
                    menuItems.Remove(continueItem);
                    
                this.AddItem(continueItem);
            }

            foreach (var menuItem in menuItems)
                menuDialog.AddItem(menuItem.Name);

            menuDialog.Show(player);
        }

        private void MenuResponse(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                MenuDialogEventArgs eventArgs = new MenuDialogEventArgs();
                eventArgs.Data = itemsData;
                eventArgs.Button = DialogButton.Right;
                OnEnded(eventArgs);
                return;
            }

            if (e.ListItem <= menuItems.Count)
            {
                MenuDialogItemEventArgs eventArgs = new MenuDialogItemEventArgs();
                eventArgs.Parent = this;
                eventArgs.ParentData = itemsData;
                eventArgs.Player = e.Player;
                menuItems[e.ListItem].OnSelected(eventArgs);
                return;
            }

            this.Show(e.Player);
        }

        private void ContinueResponse(object sender, MenuDialogItemEventArgs e)
        {
            MenuDialogEventArgs eventArgs = new MenuDialogEventArgs();
            eventArgs.Button = DialogButton.Left;
            eventArgs.Data = itemsData;

            OnEnded(eventArgs);
        }
    }
}
