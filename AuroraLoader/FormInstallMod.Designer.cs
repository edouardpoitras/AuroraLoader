namespace AuroraLoader
{
    partial class FormInstallMod
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
            this.TextUrl = new System.Windows.Forms.TextBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LabelUrl = new System.Windows.Forms.Label();
            this.ComboMods = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TextUrl
            // 
            this.TextUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextUrl.Location = new System.Drawing.Point(56, 116);
            this.TextUrl.Name = "TextUrl";
            this.TextUrl.Size = new System.Drawing.Size(658, 26);
            this.TextUrl.TabIndex = 0;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOk.Location = new System.Drawing.Point(56, 173);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(206, 79);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.Text = "Install";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel.Location = new System.Drawing.Point(292, 173);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(206, 79);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LabelUrl
            // 
            this.LabelUrl.AutoSize = true;
            this.LabelUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUrl.Location = new System.Drawing.Point(52, 93);
            this.LabelUrl.Name = "LabelUrl";
            this.LabelUrl.Size = new System.Drawing.Size(214, 20);
            this.LabelUrl.TabIndex = 3;
            this.LabelUrl.Text = "Enter the mod installation url:";
            // 
            // ComboMods
            // 
            this.ComboMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboMods.FormattingEnabled = true;
            this.ComboMods.Location = new System.Drawing.Point(56, 62);
            this.ComboMods.Name = "ComboMods";
            this.ComboMods.Size = new System.Drawing.Size(442, 28);
            this.ComboMods.TabIndex = 4;
            // 
            // FormInstallMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 274);
            this.Controls.Add(this.ComboMods);
            this.Controls.Add(this.LabelUrl);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TextUrl);
            this.Name = "FormInstallMod";
            this.Text = "Install Mods";
            this.Load += new System.EventHandler(this.FormInstallMod_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextUrl;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label LabelUrl;
        private System.Windows.Forms.ComboBox ComboMods;
    }
}