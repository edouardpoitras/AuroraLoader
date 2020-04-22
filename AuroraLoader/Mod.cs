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
        public enum ModType { EXE, DB, UTILITY, ROOT_UTILITY }
        public enum ModStatus { POWERUSER, PUBLIC, APPROVED }

        public static Mod BaseGame { get { return new Mod() { Name = "Base Game" }; } }

        public static List<Mod> GetInstalledMods()
        {
            var mods = new List<Mod>();

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                var mod = GetMod(file);
                mods.Add(mod);
            }

            return mods;
        }

        public static Mod GetMod(string file)
        {
            var str = File.ReadAllText(file);
            var mod = new Mod() { DefFile = file };
            mod.SetConfig(str);
            
            return mod;
        }

        public string DefFile { get; private set; } = null;
        public string Name { get; private set; } = null;
        public ModType Type { get; private set; } = ModType.EXE;
        public string Version { get; private set; } = null;
        public string AuroraVersion { get; private set; } = null;
        public string Exe { get; private set; } = null;
        public string ConfigFile { get; private set; } = null;
        public ModStatus Status { get; private set; } = ModStatus.POWERUSER;
        public string Updates { get; private set; } = null;

        public void SetConfig(string config)
        {
            var settings = Config.FromString(config);

            foreach (var kvp in settings)
            {
                var key = kvp.Key;
                var val = kvp.Value;

                if (key.Equals("Name"))
                {
                    if (val.Equals("Base Game"))
                    {
                        throw new Exception("Mod name can not be Base Game");
                    }

                    Name = val;
                }
                else if (key.Equals("Status"))
                {
                    if (val.Equals("Approved"))
                    {
                        Status = Mod.ModStatus.APPROVED;
                    }
                    else if (val.Equals("Public"))
                    {
                        Status = Mod.ModStatus.PUBLIC;
                    }
                    else if (val.Equals("Poweruser"))
                    {
                        Status = Mod.ModStatus.POWERUSER;
                    }
                    else
                    {
                        throw new Exception("Invalid mod status");
                    }
                }
                else if (key.Equals("Exe"))
                {
                    if (val.Equals("Aurora.exe"))
                    {
                        throw new Exception("Mod exe can not be Aurora.exe");
                    }

                    Exe = val;
                }
                else if (key.Equals("Updates"))
                {
                    Updates = val;
                }
                else if (key.Equals("Version"))
                {
                    Version = val;
                }
                else if (key.Equals("AuroraVersion"))
                {
                    AuroraVersion = val;
                }
                else if (key.Equals("Config"))
                {
                    ConfigFile = val;
                }
                else if (key.Equals("Type"))
                {
                    if (val.Equals("Exe"))
                    {
                        Type = ModType.EXE;
                    }
                    else if (val.Equals("DB"))
                    {
                        Type = ModType.DB;
                    }
                    else if (val.Equals("Utility"))
                    {
                        Type = ModType.UTILITY;
                    }
                    else if (val.Equals("RootUtility"))
                    {
                        Type = ModType.ROOT_UTILITY;
                    }
                    else
                    {
                        throw new Exception("Invalid mod type: " + val);
                    }
                }
                else
                {
                    throw new Exception("Invalid config line");
                }
            }

            if (Name == null || Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2");
            }
        }

        public bool WorksForVersion(string version)
        {
            return (version + ".").StartsWith(AuroraVersion + ".");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Config: " + DefFile);
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
