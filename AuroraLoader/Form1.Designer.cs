namespace AuroraLoader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupMods = new System.Windows.Forms.GroupBox();
            this.ListDBMods = new System.Windows.Forms.CheckedListBox();
            this.LabelDBMods = new System.Windows.Forms.Label();
            this.LabelExeMod = new System.Windows.Forms.Label();
            this.ComboExe = new System.Windows.Forms.ComboBox();
            this.CheckPower = new System.Windows.Forms.CheckBox();
            this.CheckPublic = new System.Windows.Forms.CheckBox();
            this.CheckApproved = new System.Windows.Forms.CheckBox();
            this.CheckMods = new System.Windows.Forms.CheckBox();
            this.ButtonLaunch = new System.Windows.Forms.Button();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelChecksum = new System.Windows.Forms.Label();
            this.ButtonForums = new System.Windows.Forms.Button();
            this.ButtonUtilities = new System.Windows.Forms.Button();
            this.ButtonBugs = new System.Windows.Forms.Button();
            this.ButtonUpdates = new System.Windows.Forms.Button();
            this.GroupMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupMods
            // 
            this.GroupMods.Controls.Add(this.ListDBMods);
            this.GroupMods.Controls.Add(this.LabelDBMods);
            this.GroupMods.Controls.Add(this.LabelExeMod);
            this.GroupMods.Controls.Add(this.ComboExe);
            this.GroupMods.Controls.Add(this.CheckPower);
            this.GroupMods.Controls.Add(this.CheckPublic);
            this.GroupMods.Controls.Add(this.CheckApproved);
            this.GroupMods.Enabled = false;
            this.GroupMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupMods.Location = new System.Drawing.Point(341, 50);
            this.GroupMods.Name = "GroupMods";
            this.GroupMods.Size = new System.Drawing.Size(419, 446);
            this.GroupMods.TabIndex = 0;
            this.GroupMods.TabStop = false;
            this.GroupMods.Text = "Mods";
            // 
            // ListDBMods
            // 
            this.ListDBMods.FormattingEnabled = true;
            this.ListDBMods.Location = new System.Drawing.Point(10, 150);
            this.ListDBMods.Name = "ListDBMods";
            this.ListDBMods.Size = new System.Drawing.Size(403, 277);
            this.ListDBMods.TabIndex = 6;
            // 
            // LabelDBMods
            // 
            this.LabelDBMods.AutoSize = true;
            this.LabelDBMods.Location = new System.Drawing.Point(6, 114);
            this.LabelDBMods.Name = "LabelDBMods";
            this.LabelDBMods.Size = new System.Drawing.Size(126, 20);
            this.LabelDBMods.TabIndex = 5;
            this.LabelDBMods.Text = "Database mods:";
            // 
            // LabelExeMod
            // 
            this.LabelExeMod.AutoSize = true;
            this.LabelExeMod.Location = new System.Drawing.Point(2, 75);
            this.LabelExeMod.Name = "LabelExeMod";
            this.LabelExeMod.Size = new System.Drawing.Size(75, 20);
            this.LabelExeMod.TabIndex = 4;
            this.LabelExeMod.Text = "Exe mod:";
            // 
            // ComboExe
            // 
            this.ComboExe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboExe.FormattingEnabled = true;
            this.ComboExe.Items.AddRange(new object[] {
            "Base Game"});
            this.ComboExe.Location = new System.Drawing.Point(83, 72);
            this.ComboExe.Name = "ComboExe";
            this.ComboExe.Size = new System.Drawing.Size(326, 28);
            this.ComboExe.TabIndex = 3;
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckPower.Location = new System.Drawing.Point(270, 25);
            this.CheckPower.Name = "CheckPower";
            this.CheckPower.Size = new System.Drawing.Size(146, 24);
            this.CheckPower.TabIndex = 2;
            this.CheckPower.Text = "Poweruser mods";
            this.CheckPower.UseVisualStyleBackColor = true;
            this.CheckPower.CheckedChanged += new System.EventHandler(this.CheckPower_CheckedChanged);
            // 
            // CheckPublic
            // 
            this.CheckPublic.AutoSize = true;
            this.CheckPublic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckPublic.Location = new System.Drawing.Point(151, 25);
            this.CheckPublic.Name = "CheckPublic";
            this.CheckPublic.Size = new System.Drawing.Size(113, 24);
            this.CheckPublic.TabIndex = 2;
            this.CheckPublic.Text = "Public mods";
            this.CheckPublic.UseVisualStyleBackColor = true;
            this.CheckPublic.CheckedChanged += new System.EventHandler(this.CheckPublic_CheckedChanged);
            // 
            // CheckApproved
            // 
            this.CheckApproved.AutoSize = true;
            this.CheckApproved.Checked = true;
            this.CheckApproved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckApproved.Location = new System.Drawing.Point(6, 25);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(139, 24);
            this.CheckApproved.TabIndex = 2;
            this.CheckApproved.Text = "Approved mods";
            this.CheckApproved.UseVisualStyleBackColor = true;
            this.CheckApproved.CheckedChanged += new System.EventHandler(this.CheckApproved_CheckedChanged);
            // 
            // CheckMods
            // 
            this.CheckMods.AutoSize = true;
            this.CheckMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckMods.Location = new System.Drawing.Point(341, 20);
            this.CheckMods.Name = "CheckMods";
            this.CheckMods.Size = new System.Drawing.Size(121, 24);
            this.CheckMods.TabIndex = 1;
            this.CheckMods.Text = "Enable mods";
            this.CheckMods.UseVisualStyleBackColor = true;
            this.CheckMods.CheckedChanged += new System.EventHandler(this.CheckMods_CheckedChanged);
            // 
            // ButtonLaunch
            // 
            this.ButtonLaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLaunch.Location = new System.Drawing.Point(29, 111);
            this.ButtonLaunch.Name = "ButtonLaunch";
            this.ButtonLaunch.Size = new System.Drawing.Size(269, 46);
            this.ButtonLaunch.TabIndex = 2;
            this.ButtonLaunch.Text = "Launch Aurora";
            this.ButtonLaunch.UseVisualStyleBackColor = true;
            this.ButtonLaunch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelVersion.Location = new System.Drawing.Point(25, 24);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(186, 20);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Aurora version: Unknown";
            // 
            // LabelChecksum
            // 
            this.LabelChecksum.AutoSize = true;
            this.LabelChecksum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelChecksum.Location = new System.Drawing.Point(25, 50);
            this.LabelChecksum.Name = "LabelChecksum";
            this.LabelChecksum.Size = new System.Drawing.Size(137, 20);
            this.LabelChecksum.TabIndex = 8;
            this.LabelChecksum.Text = "Aurora checksum:";
            // 
            // ButtonForums
            // 
            this.ButtonForums.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonForums.Location = new System.Drawing.Point(29, 252);
            this.ButtonForums.Name = "ButtonForums";
            this.ButtonForums.Size = new System.Drawing.Size(269, 46);
            this.ButtonForums.TabIndex = 9;
            this.ButtonForums.Text = "Forums";
            this.ButtonForums.UseVisualStyleBackColor = true;
            this.ButtonForums.Click += new System.EventHandler(this.ButtonForums_Click);
            // 
            // ButtonUtilities
            // 
            this.ButtonUtilities.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUtilities.Location = new System.Drawing.Point(29, 304);
            this.ButtonUtilities.Name = "ButtonUtilities";
            this.ButtonUtilities.Size = new System.Drawing.Size(269, 46);
            this.ButtonUtilities.TabIndex = 10;
            this.ButtonUtilities.Text = "Utilities";
            this.ButtonUtilities.UseVisualStyleBackColor = true;
            this.ButtonUtilities.Click += new System.EventHandler(this.ButtonUtilities_Click);
            // 
            // ButtonBugs
            // 
            this.ButtonBugs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonBugs.Location = new System.Drawing.Point(29, 356);
            this.ButtonBugs.Name = "ButtonBugs";
            this.ButtonBugs.Size = new System.Drawing.Size(269, 46);
            this.ButtonBugs.TabIndex = 11;
            this.ButtonBugs.Text = "Bug reports";
            this.ButtonBugs.UseVisualStyleBackColor = true;
            this.ButtonBugs.Click += new System.EventHandler(this.ButtonBugs_Click);
            // 
            // ButtonUpdates
            // 
            this.ButtonUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUpdates.Location = new System.Drawing.Point(29, 200);
            this.ButtonUpdates.Name = "ButtonUpdates";
            this.ButtonUpdates.Size = new System.Drawing.Size(269, 46);
            this.ButtonUpdates.TabIndex = 12;
            this.ButtonUpdates.Text = "Check for updates";
            this.ButtonUpdates.UseVisualStyleBackColor = true;
            this.ButtonUpdates.Click += new System.EventHandler(this.ButtonUpdates_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 508);
            this.Controls.Add(this.ButtonUpdates);
            this.Controls.Add(this.ButtonBugs);
            this.Controls.Add(this.ButtonUtilities);
            this.Controls.Add(this.ButtonForums);
            this.Controls.Add(this.LabelChecksum);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.ButtonLaunch);
            this.Controls.Add(this.CheckMods);
            this.Controls.Add(this.GroupMods);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupMods.ResumeLayout(false);
            this.GroupMods.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupMods;
        private System.Windows.Forms.CheckBox CheckPower;
        private System.Windows.Forms.CheckBox CheckPublic;
        private System.Windows.Forms.CheckBox CheckApproved;
        private System.Windows.Forms.CheckBox CheckMods;
        private System.Windows.Forms.Label LabelDBMods;
        private System.Windows.Forms.Label LabelExeMod;
        private System.Windows.Forms.ComboBox ComboExe;
        private System.Windows.Forms.CheckedListBox ListDBMods;
        private System.Windows.Forms.Button ButtonLaunch;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label LabelChecksum;
        private System.Windows.Forms.Button ButtonForums;
        private System.Windows.Forms.Button ButtonUtilities;
        private System.Windows.Forms.Button ButtonBugs;
        private System.Windows.Forms.Button ButtonUpdates;
    }
}

