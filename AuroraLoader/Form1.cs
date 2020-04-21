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
        private readonly List<Mod> AvailableExeMods = new List<Mod>();

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
            CheckMods.Enabled = false;
            CheckMods.Refresh();

            var exe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ApplyExeMod(ComboExe.SelectedItem as Mod));
            Process.Start(new ProcessStartInfo(exe));
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
            var checksum = Versions.GetAuroraChecksum();
            LabelChecksum.Text = "Aurora checksum: " + checksum;

            Version = Versions.GetAuroraVersion(out string highest);
            if (Version == null)
            {
                Version = "Unknown";
                CheckMods.Enabled = false;
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
            Mods.Add(new Mod() { Name = "Base Game", Status = Mod.ModStatus.APPROVED });

            var mods = Mod.GetMods();
            foreach (var mod in mods)
            {
                if (mod.WorksForVersion(Version))
                {
                    Mods.Add(mod);
                }
            }
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
                foreach (var kvp in Updater.GetUpdateUrls())
                {
                    Debug.WriteLine("Updating: " + kvp.Key.Name + " at " + kvp.Value);
                    Updater.Update(kvp.Key, kvp.Value);
                }
            }

            Debug.WriteLine("Stop updating");

            LoadVersion();
            LoadMods();
            UpdateLists();

            Cursor = Cursors.Default;
        }
    }
}
