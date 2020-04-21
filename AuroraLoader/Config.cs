using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    public static class Config
    {
        public static Dictionary<string, string> FromString(string str)
        {
            var result = new Dictionary<string, string>();

            var lines = str.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => l.Length > 0)
                .Where(l => !l.StartsWith(";"))
                .ToList();

            foreach (var line in lines)
            {
                if (!line.Contains("="))
                {
                    throw new Exception("Invalid config file line: " + line);
                }

                var index = line.IndexOf('=');
                var name = line.Substring(0, index);
                var val = line.Substring(index + 1);

                result.Add(name, val);
            }

            return result;
        }

        public static string ToString(Dictionary<string, string> values)
        {
            var sb = new StringBuilder();

            var lines = new List<string>();

            foreach (var kvp in values)
            {
                lines.Add(kvp.Key + "=" + kvp.Value);
            }

            lines.Sort();

            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
