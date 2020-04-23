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
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

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
