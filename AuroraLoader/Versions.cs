using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    public static class Versions
    {
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
            var pieces = a.Split('.');
            var major_a = int.Parse(pieces[0]);
            var minor_a = int.Parse(pieces[1]);
            var rev_a = int.Parse(pieces[2]);

            pieces = b.Split('.');
            var major_b = int.Parse(pieces[0]);
            var minor_b = int.Parse(pieces[1]);
            var rev_b = int.Parse(pieces[2]);

            if (major_a < major_b)
            {
                return -1;
            }
            else if (major_a > major_b)
            {
                return 1;
            }
            else
            {
                if (minor_a < minor_b)
                {
                    return -1;
                }
                else if (minor_a > minor_b)
                {
                    return 1;
                }
                else
                {
                    if (rev_a < rev_b)
                    {
                        return -1;
                    }
                    else if (rev_a > rev_b)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
