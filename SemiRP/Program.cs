using SampSharp.Core;
using System.IO;
using System.Reflection;
using System.Text;

namespace SemiRP
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            new GameModeBuilder()
                .Use<GameMode>()
                .Run();
#else
            new GameModeBuilder()
                .Use<GameMode>()
                .UseEncoding(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..\\codepages\\cp1252.txt"))
                .Run();
#endif
        }
    }
}
