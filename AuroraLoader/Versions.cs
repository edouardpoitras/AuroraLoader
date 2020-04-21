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

        public static int Compare2(string a, string b)
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
