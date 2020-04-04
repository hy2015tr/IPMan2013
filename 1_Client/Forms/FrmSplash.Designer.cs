namespace IPMAN2013
{
	partial class FrmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbSupervisors = new System.Windows.Forms.Label();
            this.lbDevelopers = new System.Windows.Forms.Label();
            this.lbTestQuality = new System.Windows.Forms.Label();
            this.lbBeta = new System.Windows.Forms.Label();
            this.pnSplash = new System.Windows.Forms.Panel();
            this.pnAppVersion = new System.Windows.Forms.Panel();
            this.lbBolum = new DevExpress.XtraEditors.LabelControl();
            this.lbApplication = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureSplash = new System.Windows.Forms.PictureBox();
            this.picLogo = new DevExpress.XtraEditors.PictureEdit();
            this.pnSplash.SuspendLayout();
            this.pnAppVersion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSplash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.BackColor = System.Drawing.Color.White;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbVersion.Location = new System.Drawing.Point(4, 62);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(60, 16);
            this.lbVersion.TabIndex = 3;
            this.lbVersion.Text = "v1.0.0.0";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbVersion.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbCopyright.Location = new System.Drawing.Point(5, 162);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(242, 13);
            this.lbCopyright.TabIndex = 32;
            this.lbCopyright.Text = "Copyright 2013 © TT. All Rights Reserved.";
            this.lbCopyright.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbSupervisors
            // 
            this.lbSupervisors.AutoSize = true;
            this.lbSupervisors.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbSupervisors.Location = new System.Drawing.Point(8, 107);
            this.lbSupervisors.Name = "lbSupervisors";
            this.lbSupervisors.Size = new System.Drawing.Size(210, 13);
            this.lbSupervisors.TabIndex = 33;
            this.lbSupervisors.Text = "Management :  Ihsan Bozdag / Engin Uzun";
            this.lbSupervisors.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbDevelopers
            // 
            this.lbDevelopers.AutoSize = true;
            this.lbDevelopers.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbDevelopers.Location = new System.Drawing.Point(8, 122);
            this.lbDevelopers.Name = "lbDevelopers";
            this.lbDevelopers.Size = new System.Drawing.Size(226, 13);
            this.lbDevelopers.TabIndex = 35;
            this.lbDevelopers.Text = "Developers :  Hasan Yildirim / Gokhan Yenigun";
            this.lbDevelopers.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbTestQuality
            // 
            this.lbTestQuality.AutoSize = true;
            this.lbTestQuality.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbTestQuality.Location = new System.Drawing.Point(8, 137);
            this.lbTestQuality.Name = "lbTestQuality";
            this.lbTestQuality.Size = new System.Drawing.Size(248, 13);
            this.lbTestQuality.TabIndex = 36;
            this.lbTestQuality.Text = "Analys && Design :  Furkan Acik / Guven Gucuyener";
            // 
            // lbBeta
            // 
            this.lbBeta.AutoSize = true;
            this.lbBeta.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lbBeta.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbBeta.Location = new System.Drawing.Point(70, 66);
            this.lbBeta.Name = "lbBeta";
            this.lbBeta.Size = new System.Drawing.Size(72, 11);
            this.lbBeta.TabIndex = 44;
            this.lbBeta.Text = "[ Final Release ]";
            // 
            // pnSplash
            // 
            this.pnSplash.BackColor = System.Drawing.Color.White;
            this.pnSplash.Controls.Add(this.pnAppVersion);
            this.pnSplash.Controls.Add(this.lbTestQuality);
            this.pnSplash.Controls.Add(this.lbCopyright);
            this.pnSplash.Controls.Add(this.lbDevelopers);
            this.pnSplash.Controls.Add(this.lbSupervisors);
            this.pnSplash.Location = new System.Drawing.Point(260, 12);
            this.pnSplash.Name = "pnSplash";
            this.pnSplash.Size = new System.Drawing.Size(265, 185);
            this.pnSplash.TabIndex = 45;
            // 
            // pnAppVersion
            // 
            this.pnAppVersion.Controls.Add(this.lbBolum);
            this.pnAppVersion.Controls.Add(this.lbApplication);
            this.pnAppVersion.Controls.Add(this.lbVersion);
            this.pnAppVersion.Controls.Add(this.lbBeta);
            this.pnAppVersion.Location = new System.Drawing.Point(3, 3);
            this.pnAppVersion.Name = "pnAppVersion";
            this.pnAppVersion.Size = new System.Drawing.Size(247, 93);
            this.pnAppVersion.TabIndex = 45;
            // 
            // lbBolum
            // 
            this.lbBolum.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbBolum.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lbBolum.Location = new System.Drawing.Point(8, 41);
            this.lbBolum.Name = "lbBolum";
            this.lbBolum.Size = new System.Drawing.Size(119, 18);
            this.lbBolum.TabIndex = 53;
            this.lbBolum.Text = "Powered by CIS";
            // 
            // lbApplication
            // 
            this.lbApplication.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.lbApplication.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbApplication.Location = new System.Drawing.Point(7, 6);
            this.lbApplication.Name = "lbApplication";
            this.lbApplication.Size = new System.Drawing.Size(184, 39);
            this.lbApplication.TabIndex = 50;
            this.lbApplication.Text = "IPMan2013";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::IPMAN2013.Properties.Resources.PBN2013_Logo;
            this.pictureEdit1.Location = new System.Drawing.Point(142, 174);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(94, 23);
            this.pictureEdit1.TabIndex = 49;
            // 
            // pictureSplash
            // 
            this.pictureSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureSplash.Image = ((System.Drawing.Image)(resources.GetObject("pictureSplash.Image")));
            this.pictureSplash.Location = new System.Drawing.Point(0, 0);
            this.pictureSplash.Name = "pictureSplash";
            this.pictureSplash.Size = new System.Drawing.Size(539, 209);
            this.pictureSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureSplash.TabIndex = 0;
            this.pictureSplash.TabStop = false;
            this.pictureSplash.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // picLogo
            // 
            this.picLogo.EditValue = global::IPMAN2013.Properties.Resources.TT01;
            this.picLogo.Location = new System.Drawing.Point(51, 71);
            this.picLogo.Name = "picLogo";
            this.picLogo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.picLogo.Properties.Appearance.Options.UseBackColor = true;
            this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picLogo.Size = new System.Drawing.Size(149, 61);
            this.picLogo.TabIndex = 50;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(539, 209);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.pnSplash);
            this.Controls.Add(this.pictureSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.Text = "frmSplash";
            this.pnSplash.ResumeLayout(false);
            this.pnSplash.PerformLayout();
            this.pnAppVersion.ResumeLayout(false);
            this.pnAppVersion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSplash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureSplash;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.Label lbSupervisors;
        private System.Windows.Forms.Label lbDevelopers;
        private System.Windows.Forms.Label lbTestQuality;
        private System.Windows.Forms.Label lbBeta;
        private System.Windows.Forms.Panel pnSplash;
        private System.Windows.Forms.Panel pnAppVersion;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lbApplication;
        private DevExpress.XtraEditors.LabelControl lbBolum;
        private DevExpress.XtraEditors.PictureEdit picLogo;
    }
}