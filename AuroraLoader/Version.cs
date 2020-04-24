using Semver;
using System;

namespace AuroraLoader
{
    public class Version : IComparable<Version>
    {
        public SemVersion GameVersion { get; }
        public string Checksum { get; }

        public Version(string version, string checksum)
        {
            if (version[0] == 65279)
            {
                version = version.Substring(1);
            }

            GameVersion = version;
            Checksum = checksum;
        }

        public int CompareTo(Version other)
        {
            return GameVersion.CompareTo(other);
        }
    }
}
