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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class Form1 : Form
    {
        private string Version { get; set; } = null;
        private readonly List<Mod> Mods = new List<Mod>();

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckMods_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMods.Checked)
            {
                var result = MessageBox.Show("By using mods you agree to not post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    GroupMods.Enabled = true;
                    ButtonUpdateMods.Enabled = true;
                    ButtonBugs.Enabled = false;
                }
                else
                {
                    CheckMods.Checked = false;
                }
            }
            else
            {
                GroupMods.Enabled = false;
                ButtonUpdateMods.Enabled = false;
                ButtonBugs.Enabled = true;
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


            var exe = Mod.BaseGame;
            var others = new List<Mod>();

            if (CheckMods.Checked)
            {
                exe = ComboExe.SelectedItem as Mod;

                for (int i = 0; i < ListDBMods.CheckedItems.Count; i++)
                {
                    others.Add(ListDBMods.CheckedItems[i] as Mod);
                }
            }

            CheckMods.Enabled = false;
            CheckMods.Refresh();

            Launcher.Launch(exe, others);
        }

        private void UpdateLists()
        {
            
            var status_approved = CheckApproved.Checked;
            var status_public = CheckPublic.Checked;
            var status_poweruser = CheckPower.Checked;

            var exes = new List<Mod>();
            var other = new List<Mod>();
            
            if (status_approved)
            {
                exes.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.APPROVED));
                other.AddRange(Mods.Where(m => m.Type != Mod.ModType.EXE && m.Status == Mod.ModStatus.APPROVED));
            }
            
            if (status_public)
            {
                exes.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.PUBLIC));
                other.AddRange(Mods.Where(m => m.Type != Mod.ModType.EXE && m.Status == Mod.ModStatus.PUBLIC));
            }

            if (status_poweruser)
            {
                exes.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.POWERUSER));
                other.AddRange(Mods.Where(m => m.Type != Mod.ModType.EXE && m.Status == Mod.ModStatus.POWERUSER));
            }

            exes.Sort((a, b) => a.Name.CompareTo(b.Name));
            exes.Insert(0, Mod.BaseGame);

            var selected_exe = (Mod)ComboExe.SelectedItem;
            ComboExe.Items.Clear();
            ComboExe.Items.AddRange(exes.ToArray());

            if (exes.Contains(selected_exe))
            {
                ComboExe.SelectedItem = selected_exe;
            }
            else
            {
                ComboExe.SelectedIndex = 0;
            }

            other.Sort((a, b) => a.Name.CompareTo(b.Name));

            var selected_others = new List<int>();
            for (int i = 0; i < ListDBMods.CheckedItems.Count; i++)
            {
                for (int j = 0; j < other.Count; j++)
                {
                    if (ListDBMods.CheckedItems[i].Equals(other[j]))
                    {
                        selected_others.Add(j);
                    }
                }
            }

            ListDBMods.Items.Clear();
            ListDBMods.Items.AddRange(other.ToArray());
            foreach (var index in selected_others)
            {
                ListDBMods.SetItemChecked(index, true);
            }

            Debug.WriteLine("exes: " + exes.Count);
        }

        private void LoadVersion()
        {
            var checksum = Versions.GetAuroraChecksum();
            LabelChecksum.Text = "Aurora checksum: " + checksum;

            Version = Versions.GetAuroraVersion(out string highest);
            if (Version == null)
            {
                Version = "Unknown";
                CheckMods.Enabled = false;
            }

            if (highest.Equals("0.0.0"))
            {
                highest = "Unknown";
            }

            LabelVersion.Text = "Aurora version: " + Version;

            if (!Version.Equals(highest))
            {
                ButtonUpdates.Text = "Check for updates: " + highest;
                ButtonUpdates.ForeColor = Color.Green;
            }
        }

        private void LoadMods()
        {
            Mods.Clear();

            var mods = Mod.GetInstalledMods();
            var latest = new Dictionary<string, Mod>();

            foreach (var mod in mods)
            {
                if (mod.WorksForVersion(Version))
                {
                    if (!latest.ContainsKey(mod.Name))
                    {
                        latest.Add(mod.Name, mod);
                    }
                    else if (Versions.IsHigher(mod.Version, latest[mod.Name].Version))
                    {
                        latest[mod.Name] = mod;
                    }
                }
            }

            Mods.AddRange(latest.Values);
        }

        private string ApplyExeMod(Mod mod)
        {
            if (mod.Name.Equals("Base Game"))
            {
                return "Aurora.exe";
            }
            else
            {
                var dir = Path.GetDirectoryName(mod.DefFile);
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

        private void ButtonMods_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonUpdateMods_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Debug.WriteLine("Start updating");

            var urls = Updater.GetUpdateUrls();
            if (urls.Count == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("All mods are up to date");
            }
            else
            {
                foreach (var kvp in urls)
                {
                    Debug.WriteLine("Updating: " + kvp.Key.Name + " at " + kvp.Value);
                    Updater.Update(kvp.Key, kvp.Value);
                }

                Cursor = Cursors.Default;
                MessageBox.Show("Updated " + urls.Count + " mods.");
            }

            Debug.WriteLine("Stop updating");

            LoadVersion();
            LoadMods();
            UpdateLists();
        }

        private void ComboExe_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (Mod)ComboExe.SelectedItem;

            if (selected.ConfigFile == null)
            {
                ButtonConfigureMod.Enabled = false;
            }
            else
            {
                ButtonConfigureMod.Enabled = true;
            }
        }

        private void ButtonConfigureMod_Click(object sender, EventArgs e)
        {
            var selected = (Mod)ComboExe.SelectedItem;

            var file = Path.Combine(Path.GetDirectoryName(selected.DefFile), selected.ConfigFile);
            Process.Start(file);
        }
    }
}
