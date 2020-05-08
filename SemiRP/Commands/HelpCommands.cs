using SampSharp.Core.Callbacks;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SemiRP.Commands
{
    class HelpCommands
    {
        [Command("aide", "help", "h")]
        private static void Help(Player sender, string command = "")
        {
            CommandsManager cmdManager = (CommandsManager)GameMode.Instance.Services.GetService<ICommandsManager>();

            if (command != "")
            {
                DefaultCommand cmd = (DefaultCommand)cmdManager.GetCommandForText(sender, command);

                if (cmd != null)
                    Utils.Chat.InfoChat(sender, cmd.UsageMessage);
                else if (cmdManager.Commands.Any(c => ((DefaultCommand)c).Names[0].Group == command))
                {
                    Utils.Chat.HelpChat(sender, "--- " + Constants.Chat.HIGHLIGHT + command + Color.White + " ---");
                    Utils.CmdsHelper.ShowCommandListForPlayer(sender, Utils.CmdsHelper.ListAllCommandsInGroup(command));
                }
                else
                    Utils.Chat.InfoChat(sender, "Cette commande n'éxiste pas.");

                return;
            }

            {
                var globalGroup = ((GameMode)GameMode.Instance).GroupedCommands["global"];

                Utils.Chat.HelpChat(sender, "--- " + Constants.Chat.HIGHLIGHT + "global" + Color.White + " ---");
                Utils.CmdsHelper.ShowCommandListForPlayer(sender, globalGroup.Select(c => c.Names[0].Name));
            }

            foreach (KeyValuePair<string, List<DefaultCommand>> cmdGroup in ((GameMode)GameMode.Instance).GroupedCommands.OrderBy(kv => kv.Key).ToList())
            {
                if (cmdGroup.Value.Count == 0)
                    continue;

                if (cmdGroup.Key == "global")
                    continue;

                Utils.Chat.HelpChat(sender, "--- " + Constants.Chat.HIGHLIGHT + cmdGroup.Key + Color.White + " ---");
                Utils.CmdsHelper.ShowCommandListForPlayer(sender, Utils.CmdsHelper.ListAllCommandsInGroup(cmdGroup.Key));
            }
        }
    }
}
