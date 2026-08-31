using System.Globalization;
using System.Runtime.Versioning;
using WaterStation.Forms;

[assembly: SupportedOSPlatform("windows")]

namespace WaterStation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var arabicCulture = new CultureInfo("ar-SA");
            CultureInfo.DefaultThreadCurrentCulture = arabicCulture;
            CultureInfo.DefaultThreadCurrentUICulture = arabicCulture;

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}