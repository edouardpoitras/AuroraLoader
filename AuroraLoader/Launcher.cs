using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    static class Launcher
    {
        public static void Launch(Mod exe, List<Mod> others)
        {
            foreach (var mod in others)
            {
                if (mod.Type == Mod.ModType.ROOT_UTILITY)
                {
                    Debug.WriteLine("Root Utility: " + mod.Name);
                    CopyToRoot(mod);
                    Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mod.Exe));
                }
                else if (mod.Type == Mod.ModType.UTILITY)
                {
                    Debug.WriteLine("Utility: " + mod.Name);
                    Process.Start(Path.Combine(Path.GetDirectoryName(mod.DefFile), mod.Exe));
                }
                else if (mod.Type == Mod.ModType.DATABASE)
                {
                    Debug.WriteLine("Database: " + mod.Name);
                    throw new Exception("Database mods not supported yet: " + mod.Name);
                }
                else
                {
                    throw new Exception("Invalid mod: " + mod.Name);
                }
            }

            if (exe.Name.Equals("Base Game"))
            {
                Debug.WriteLine("Exe: " + exe.Name);
                Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe"));
            }
            else
            {
                Debug.WriteLine("Exe: " + exe.Name);
                CopyToRoot(exe);
                Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe.Exe));
            }
        }

        private static void CopyToRoot(Mod mod)
        {
            var dir = Path.GetDirectoryName(mod.DefFile);
            var out_dir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.ini")))
            {
                File.Copy(file, Path.Combine(out_dir, Path.GetFileName(file)), true);
            }
        }
    }
}
