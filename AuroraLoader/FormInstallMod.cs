using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class FormInstallMod : Form
    {
        public FormInstallMod()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ButtonOk.Enabled = false;
            ButtonCancel.Enabled = false;

            try
            {
                var url = "";
                using (var client = new WebClient())
                {
                    var updates = Config.FromString(client.DownloadString(TextUrl.Text));
                    var highest = new Version("0.0.0");

                    foreach (var kvp in updates)
                    {
                        var version = new Version(kvp.Key);
                        if (version.IsHigher(highest))
                        {
                            highest = version;
                            url = kvp.Value;
                        }
                    }

                }

                if (!"".Equals(url))
                {
                    Updater.Update(url);
                    MessageBox.Show("Mod installed");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to install mod");
            }

            ButtonOk.Enabled = true;
            ButtonCancel.Enabled = true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
