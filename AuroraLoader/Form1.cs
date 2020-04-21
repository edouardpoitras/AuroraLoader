using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class Form1 : Form
    {
        private string Version { get; set; } = null;
        private readonly List<Mod> Mods = new List<Mod>();
        private readonly List<Mod> AvailableExeMods = new List<Mod>();

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckMods_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMods.Checked)
            {
                var result = MessageBox.Show("By using mods you agree not to post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    GroupMods.Enabled = true;
                }
                else
                {
                    CheckMods.Checked = false;
                }
            }
            else
            {
                GroupMods.Enabled = false;
                AvailableExeMods.Clear();
                AvailableExeMods.Add(Mods.Where(m => m.Name.Equals("Base Game")).FirstOrDefault());
                ComboExe.SelectedIndex = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadVersion();
            LoadMods();
            UpdateLists();
        }

        private void ButtonLaunch_Click(object sender, EventArgs e)
        {
            ButtonLaunch.Enabled = false;
            ButtonLaunch.Refresh();

            var exe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ApplyExeMod(ComboExe.SelectedItem as Mod));
            Process.Start(new ProcessStartInfo(exe));

            Application.Exit();
        }

        private void UpdateLists()
        {
            var selected = (Mod)ComboExe.SelectedItem;
            var status_approved = CheckApproved.Checked;
            var status_public = CheckPublic.Checked;
            var status_poweruser = CheckPower.Checked;

            AvailableExeMods.Clear();
            
            if (status_approved)
            {
                AvailableExeMods.AddRange(Mods.Where(m => m.Status == Mod.ModStatus.APPROVED));
            }
            
            if (status_public)
            {
                AvailableExeMods.AddRange(Mods.Where(m => m.Status == Mod.ModStatus.PUBLIC));
            }

            if (status_poweruser)
            {
                AvailableExeMods.AddRange(Mods.Where(m => m.Status == Mod.ModStatus.POWERUSER));
            }

            AvailableExeMods.RemoveAll(m => m.Name.Equals("Base Game") || m.Exe == null);
            AvailableExeMods.Sort((a, b) => a.Name.CompareTo(b.Name));
            AvailableExeMods.Insert(0, Mods.Where(m => m.Name.Equals("Base Game")).FirstOrDefault());

            ComboExe.DisplayMember = "Name";
            ComboExe.Items.Clear();
            ComboExe.Items.AddRange(AvailableExeMods.ToArray());

            if (AvailableExeMods.Contains(selected))
            {
                ComboExe.SelectedItem = selected;
            }
            else
            {
                ComboExe.SelectedIndex = 0;
            }

            Debug.WriteLine("exes: " + AvailableExeMods.Count);
        }

        private void LoadVersion()
        {
            Version = null;
            var checksum = Versions.GetAuroraChecksum();
            LabelChecksum.Text = "Aurora checksum: " + checksum;

            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "versions.txt");
                var lines = File.ReadAllLines(file);

                for (int i = 0; i < lines.Length; i += 2)
                {
                    if (checksum.Equals(lines[i + 1]))
                    {
                        Version = lines[i];
                        LabelVersion.Text = "Aurora version: " + Version;
                    }
                }

                if (Version == null)
                {
                    using (var client = new WebClient())
                    {
                        var str = client.DownloadString("https://raw.githubusercontent.com/01010100b/AuroraLoader/master/AuroraLoader/versions.txt");
                        lines = str.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < lines.Length; i += 2)
                        {
                            if (checksum.Equals(lines[i + 1]))
                            {
                                Version = lines[i];
                                LabelVersion.Text = "Aurora version: " + Version;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Version = null;
            }
            
            if (Version == null)
            {
                CheckMods.Enabled = false;
                Version = "Unknown";
                LabelVersion.Text = "Aurora version: Unknown";
            }
        }

        private void LoadMods()
        {
            Mods.Clear();
            Mods.Add(new Mod() { Name = "Base Game", Status = Mod.ModStatus.APPROVED });

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                var mod = GetMod(file);
                Debug.WriteLine(mod);
                Debug.WriteLine("");

                if (mod.WorksForVersion(Version))
                {
                    Mods.Add(mod);
                }
            }
        }

        private Mod GetMod(string file)
        {
            var lines = File.ReadAllLines(file);
            var mod = new Mod() { ConfigFile = file };

            foreach (var line in lines.Select(l => l.Trim()).Where(l => l.Length > 0).Where(l => !l.StartsWith(";")))
            {
                if (!line.Contains("="))
                {
                    throw new Exception($"Invalid config line in {file}: {line}");
                }

                var pieces = line.Split('=');
                var key = pieces[0];
                var val = pieces[1];

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
                        throw new Exception($"Invalid status in {file}: {line}");
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
                    throw new Exception($"Invalid config line in {Path.GetFileName(file)}: {line}");
                }
            }

            if (mod.Name == null || mod.Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2: " + file);
            }

            return mod;
        }

        private string ApplyExeMod(Mod mod)
        {
            if (mod.Name.Equals("Base Game"))
            {
                return "Aurora.exe";
            }
            else
            {
                var dir = Path.GetDirectoryName(mod.ConfigFile);
                var out_dir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !f.EndsWith("mod.ini")))
                {
                    File.Copy(file, Path.Combine(out_dir, Path.GetFileName(file)), true);
                }

                return mod.Exe;
            }    
        }

        private void CheckApproved_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void CheckPublic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void CheckPower_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void ButtonForums_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?action=forum#c14");
        }

        private void ButtonUtilities_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?board=282.0");
        }

        private void ButtonBugs_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?board=273.0");
        }

        private void ButtonUpdates_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?board=276.0");
        }
    }
}
