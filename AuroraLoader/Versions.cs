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
    public static class Versions
    {
        public static string GetAuroraVersion(out string highest)
        {
            string version = null;
            highest = "0.0.0";
            var checksum = Versions.GetAuroraChecksum();

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "versions.txt");
            var lines = File.ReadAllLines(file);
            var known = lines.Length;

            for (int i = 0; i < lines.Length; i += 2)
            {
                if (checksum.Equals(lines[i + 1]))
                {
                    version = lines[i];
                }

                if (Versions.IsHigher(lines[i], highest))
                {
                    highest = lines[i];
                }
            }

            using (var client = new WebClient())
            {
                var str = client.DownloadString("https://raw.githubusercontent.com/01010100b/AuroraLoader/master/AuroraLoader/versions.txt");
                lines = str.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < lines.Length; i += 2)
                {
                    if (checksum.Equals(lines[i + 1]))
                    {
                        version = lines[i];
                    }

                    if (Versions.IsHigher(lines[i], highest))
                    {
                        highest = lines[i];
                    }
                }

                if (lines.Length > known)
                {
                    File.WriteAllLines(file, lines);
                }
            }

            return version;
        }

        public static string GetAuroraChecksum()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");
            var bytes = File.ReadAllBytes(file);
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(bytes);
                var str = Convert.ToBase64String(hash.Take(4).ToArray());

                return str.Substring(0, 6);
            }
        }

        public static bool IsHigher(string a, string b)
        {
            return Compare(a, b) == 1;
        }

        public static int Compare(string a, string b)
        {
            var pieces_a = a.Split('.');
            var pieces_b = b.Split('.');

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
    }
}
