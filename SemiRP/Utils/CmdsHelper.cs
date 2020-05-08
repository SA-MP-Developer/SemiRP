using SampSharp.GameMode.SAMP.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils
{
    public static class CmdsHelper
    {
        public static List<string> ListAllCommandsInGroup(string cmdGroup)
        {
            List<string> res = new List<string>();

            CommandsManager cmdManager = (CommandsManager)GameMode.Instance.Services.GetService<ICommandsManager>();

            foreach (DefaultCommand cmd in cmdManager.Commands.Where(c => ((DefaultCommand)c).Names[0].Group.Contains(cmdGroup)))
            {
                if (cmd.Names[0].Group == cmdGroup)
                {
                    res.Add(cmd.Names[0].Name);
                }
                else
                {
                    string sub = cmd.Names[0].Group.Substring(cmdGroup.Length + 1);
                    if (!res.Contains(sub))
                        res.Add(sub);
                }
            }

            return res;
        }

        public static void ShowCommandListForPlayer(Player player, IEnumerable<string> commands)
        {
            int count = 0;
            string text = "";
            foreach (string cmd in commands)
            {
                if (count == 10)
                {
                    text += cmd;

                    Utils.Chat.HelpChat(player, text);

                    count = 0;
                }
                else
                {
                    if (count == 0)
                        text += cmd;
                    else
                        text += " - " + cmd;
                    count++;
                }
            }

            if (count != 0)
            {
                Utils.Chat.HelpChat(player, text);
            }
        }
    }
}
