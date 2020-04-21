using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLoader
{
    class Updater
    {
        public static Dictionary<Mod, string> GetUpdateUrls()
        {
            var updaters = new Dictionary<string, Mod>();
            
            var all_mods = Mod.GetMods();
            foreach (var mod in all_mods)
            {
                if (mod.Updates != null)
                {
                    if (!updaters.ContainsKey(mod.Name))
                    {
                        updaters.Add(mod.Name, mod);
                    }
                    else if (Versions.IsHigher(mod.Version, updaters[mod.Name].Version))
                    {
                        updaters[mod.Name] = mod;
                    }
                }
            }

            var urls = new Dictionary<Mod, string>();
            var versions = new Dictionary<Mod, string>();
            using (var client = new WebClient())
            {
                foreach (var mod in updaters.Values)
                {
                    versions.Add(mod, mod.Version);

                    var updates = client.DownloadString(mod.Updates).Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var update in updates)
                    {
                        var pieces = update.Split('=');
                        var version = pieces[0];
                        var url = pieces[1];

                        if (Versions.IsHigher(version, versions[mod]))
                        {
                            versions[mod] = version;
                            urls[mod] = url;
                        }
                    }
                }
            }

            return urls;
        }

        public static void Update(Mod mod, string url)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            var file = Path.Combine(folder, "update.current");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var client = new WebClient())
            {
                client.DownloadFile(url, file);
            }

            var mod_folder = Path.Combine(folder, mod.Name);
            ZipFile.ExtractToDirectory(file, mod_folder);

            var version = Mod.GetMod(Path.Combine(mod_folder, "mod.ini")).AuroraVersion;
            var mod_version_folder = mod_folder + " " + version;
            if (Directory.Exists(mod_version_folder))
            {
                Directory.Delete(mod_version_folder, true);
            }
            ZipFile.ExtractToDirectory(file, mod_version_folder);

            File.Delete(file);
            Directory.Delete(mod_folder, true);
        }
    }
}
