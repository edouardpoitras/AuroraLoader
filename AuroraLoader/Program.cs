using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraLoader
{
    static class Program
    {
        public static readonly string[] AURORA_MIRRORS =
        {
            "https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Aurora/"
        };

        public static readonly string[] MOD_MIRRORS =
        {
            "https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/"
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine("checksum: " + Version.GetAuroraChecksum());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
