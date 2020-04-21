using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    class Mod
    {
        public enum ModStatus { POWERUSER, PUBLIC, APPROVED }

        public string ConfigFile { get; set; } = null;
        public string Name { get; set; } = null;
        public string Version { get; set; } = null;
        public string AuroraVersion { get; set; } = null;
        public ModStatus Status { get; set; } = ModStatus.POWERUSER;
        public string Exe { get; set; } = null;

        public bool WorksForVersion(string version)
        {
            return version.StartsWith(AuroraVersion);
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

            return sb.ToString();
        }
    }
}
