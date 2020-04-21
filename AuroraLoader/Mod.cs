using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    class Mod
    {
        public enum ModStatus { POWERUSER, PUBLIC, APPROVED }

        public static List<Mod> GetMods()
        {
            var mods = new List<Mod>();

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                var mod = GetMod(file);
                Debug.WriteLine(mod);
                Debug.WriteLine("");

                mods.Add(mod);
            }

            return mods;
        }

        public static Mod GetMod(string file)
        {
            var str = File.ReadAllText(file);
            var mod = new Mod() { ConfigFile = file };

            var settings = Config.FromString(str);

            foreach (var kvp in settings)
            {
                var key = kvp.Key;
                var val = kvp.Value;

                if (key.Equals("Name"))
                {
                    if (val.Equals("Base Game"))
                    {
                        throw new Exception($"Mod name can not be Base Game in {file}");
                    }

                    mod.Name = val;
                }
                else if (key.Equals("Status"))
                {
                    if (val.Equals("Approved"))
                    {
                        mod.Status = Mod.ModStatus.APPROVED;
                    }
                    else if (val.Equals("Public"))
                    {
                        mod.Status = Mod.ModStatus.PUBLIC;
                    }
                    else if (val.Equals("Poweruser"))
                    {
                        mod.Status = Mod.ModStatus.POWERUSER;
                    }
                    else
                    {
                        throw new Exception($"Invalid status in {file}");
                    }
                }
                else if (key.Equals("Exe"))
                {
                    if (val.Equals("Aurora.exe"))
                    {
                        throw new Exception($"Mod exe can not be Aurora.exe in {file}");
                    }

                    mod.Exe = val;
                }
                else if (key.Equals("Updates"))
                {
                    mod.Updates = val;
                }
                else if (key.Equals("Version"))
                {
                    mod.Version = val;
                }
                else if (key.Equals("AuroraVersion"))
                {
                    mod.AuroraVersion = val;
                }
                else
                {
                    throw new Exception($"Invalid config line in {Path.GetFileName(file)}");
                }
            }

            if (mod.Name == null || mod.Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2: " + file);
            }

            return mod;
        }

        public string ConfigFile { get; set; } = null;
        public string Name { get; set; } = null;
        public string Version { get; set; } = null;
        public string AuroraVersion { get; set; } = null;
        public ModStatus Status { get; set; } = ModStatus.POWERUSER;
        public string Exe { get; set; } = null;
        public string Updates { get; set; } = null;

        public bool WorksForVersion(string version)
        {
            return (version + ".").StartsWith(AuroraVersion + ".");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Config: " + ConfigFile);
            sb.AppendLine("Name: " + Name);
            sb.AppendLine("Version: " + Version);
            sb.AppendLine("AuroraVersion: " + AuroraVersion);
            sb.AppendLine("Status: " + Status);
            sb.AppendLine("Exe: " + Exe);
            sb.AppendLine("Updates: " + Updates);

            return sb.ToString();
        }
    }
}
