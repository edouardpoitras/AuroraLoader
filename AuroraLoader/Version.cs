using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    public class Version : IComparable<Version>, IEquatable<Version>
    {
        private static readonly string[] MIRRORS =
        {
            "https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Aurora/aurora_versions.txt",
        };

        public static Version GetAuroraVersion(out Version highest)
        {
            Version version = null;
            highest = null;

            var checksum = GetAuroraChecksum();
            var versions = GetKnownVersions();
            foreach (var kvp in versions)
            {
                if (checksum.Equals(kvp.Value))
                {
                    version = kvp.Key;
                }

                if (IsHigher(kvp.Key, highest))
                {
                    highest = kvp.Key;
                }
            }

            return version;
        }

        public static Dictionary<Version, string> GetKnownVersions()
        {
            var versions = new Dictionary<Version, string>();

            var configs = new List<string>();

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "aurora_versions.txt");
            configs.Add(File.ReadAllText(file));

            try
            {
                using (var client = new WebClient())
                {
                    foreach (var mirror in MIRRORS)
                    {
                        configs.Add(client.DownloadString(mirror));
                    }
                }
            }
            catch (Exception)
            {

            }

            foreach (var str in configs)
            {
                foreach (var kvp in Config.FromString(str))
                {
                    versions[new Version(kvp.Key)] = kvp.Value;
                }
            }

            return versions;
        }

        public static string GetAuroraChecksum()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");
            var bytes = File.ReadAllBytes(file);
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(bytes);
                var str = Convert.ToBase64String(hash);

                return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
            }
        }

        private static bool IsHigher(Version a, Version b)
        {
            return Compare(a, b) == 1;
        }

        private static int Compare(Version a, Version b)
        {
            if (a == null)
            {
                return -1;
            }
            else if (b == null)
            {
                return 1;
            }

            var str_a = a.String;
            var str_b = b.String;

            var pieces_a = str_a.Split('.');
            var pieces_b = str_b.Split('.');

            for (int i = 0; i < Math.Max(pieces_a.Length, pieces_b.Length); i++)
            {
                if (i >= pieces_a.Length)
                {
                    return -1;
                }
                else if (i >= pieces_b.Length)
                {
                    return 1;
                }
                else
                {
                    var v_a = int.Parse(pieces_a[i]);
                    var v_b = int.Parse(pieces_b[i]);

                    if (v_a < v_b)
                    {
                        return -1;
                    }
                    else if (v_a > v_b)
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }

        private readonly string String;

        public Version(string version)
        {
            if (version[0] == 65279)
            {
                version = version.Substring(1);
            }

            String = version;
        }

        public bool IsHigher(Version other)
        {
            return IsHigher(this, other);
        }

        public int CompareTo(Version other)
        {
            return Compare(this, other);
        }

        public override bool Equals(object obj)
        {
            if (obj is Version && obj != null)
            {
                return CompareTo(obj as Version) == 0;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return String;
        }

        public bool Equals(Version other)
        {
            return other != null &&
                   String == other.String;
        }

        public override int GetHashCode()
        {
            return 2096090648 + EqualityComparer<string>.Default.GetHashCode(String);
        }

        public static bool operator ==(Version left, Version right)
        {
            return EqualityComparer<Version>.Default.Equals(left, right);
        }

        public static bool operator !=(Version left, Version right)
        {
            return !(left == right);
        }
    }
}
