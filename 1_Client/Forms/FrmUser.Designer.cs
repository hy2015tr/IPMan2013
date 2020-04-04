namespace IPMAN2013
{
    partial class FrmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUser));
            this.grpLogin = new DevExpress.XtraEditors.GroupControl();
            this.txtPassNew01 = new DevExpress.XtraEditors.TextEdit();
            this.lbPassNew = new DevExpress.XtraEditors.LabelControl();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lbPassOld = new DevExpress.XtraEditors.LabelControl();
            this.txtPassOld = new DevExpress.XtraEditors.TextEdit();
            this.lbUser = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassNew02 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogin)).BeginInit();
            this.grpLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew01.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew02.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpLogin.AppearanceCaption.Options.UseFont = true;
            this.grpLogin.Controls.Add(this.txtPassNew02);
            this.grpLogin.Controls.Add(this.labelControl1);
            this.grpLogin.Controls.Add(this.txtPassNew01);
            this.grpLogin.Controls.Add(this.lbPassNew);
            this.grpLogin.Controls.Add(this.btnChange);
            this.grpLogin.Controls.Add(this.btnClear);
            this.grpLogin.Controls.Add(this.btnClose);
            this.grpLogin.Controls.Add(this.lbPassOld);
            this.grpLogin.Controls.Add(this.txtPassOld);
            this.grpLogin.Controls.Add(this.lbUser);
            this.grpLogin.Controls.Add(this.txtUserName);
            this.grpLogin.Location = new System.Drawing.Point(12, 12);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(508, 234);
            this.grpLogin.TabIndex = 4;
            this.grpLogin.Text = "User Info";
            // 
            // txtPassNew01
            // 
            this.txtPassNew01.EditValue = "";
            this.txtPassNew01.Location = new System.Drawing.Point(161, 138);
            this.txtPassNew01.Name = "txtPassNew01";
            this.txtPassNew01.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassNew01.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassNew01.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassNew01.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassNew01.Properties.Appearance.Options.UseFont = true;
            this.txtPassNew01.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassNew01.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassNew01.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassNew01.Properties.PasswordChar = '*';
            this.txtPassNew01.Size = new System.Drawing.Size(193, 22);
            this.txtPassNew01.TabIndex = 2;
            this.txtPassNew01.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // lbPassNew
            // 
            this.lbPassNew.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPassNew.Location = new System.Drawing.Point(50, 141);
            this.lbPassNew.Name = "lbPassNew";
            this.lbPassNew.Size = new System.Drawing.Size(103, 16);
            this.lbPassNew.TabIndex = 22;
            this.lbPassNew.Text = "New Password :";
            // 
            // btnChange
            // 
            this.btnChange.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnChange.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnChange.Appearance.Options.UseFont = true;
            this.btnChange.Appearance.Options.UseForeColor = true;
            this.btnChange.Location = new System.Drawing.Point(372, 41);
            this.btnChange.LookAndFeel.SkinName = "Glass Oceans";
            this.btnChange.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 85);
            this.btnChange.TabIndex = 4;
            this.btnChange.Tag = "ActionButton";
            this.btnChange.Text = "Change";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Appearance.Options.UseForeColor = true;
            this.btnClear.Location = new System.Drawing.Point(372, 141);
            this.btnClear.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Location = new System.Drawing.Point(372, 185);
            this.btnClose.LookAndFeel.SkinName = "Stardust";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbPassOld
            // 
            this.lbPassOld.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPassOld.Location = new System.Drawing.Point(57, 94);
            this.lbPassOld.Name = "lbPassOld";
            this.lbPassOld.Size = new System.Drawing.Size(96, 16);
            this.lbPassOld.TabIndex = 19;
            this.lbPassOld.Text = "Old Password :";
            // 
            // txtPassOld
            // 
            this.txtPassOld.EditValue = "";
            this.txtPassOld.Location = new System.Drawing.Point(161, 91);
            this.txtPassOld.Name = "txtPassOld";
            this.txtPassOld.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassOld.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassOld.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassOld.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassOld.Properties.Appearance.Options.UseFont = true;
            this.txtPassOld.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassOld.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassOld.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassOld.Properties.PasswordChar = '*';
            this.txtPassOld.Size = new System.Drawing.Size(193, 22);
            this.txtPassOld.TabIndex = 1;
            this.txtPassOld.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // lbUser
            // 
            this.lbUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbUser.Location = new System.Drawing.Point(76, 41);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(77, 16);
            this.lbUser.TabIndex = 21;
            this.lbUser.Text = "User Name :";
            // 
            // txtUserName
            // 
            this.txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::IPMAN2013.Properties.Settings.Default, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUserName.EditValue = global::IPMAN2013.Properties.Settings.Default.UserName;
            this.txtUserName.EnterMoveNextControl = true;
            this.txtUserName.Location = new System.Drawing.Point(161, 38);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtUserName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.Appearance.Options.UseForeColor = true;
            this.txtUserName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtUserName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUserName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Size = new System.Drawing.Size(193, 22);
            this.txtUserName.TabIndex = 0;
            // 
            // txtPassNew02
            // 
            this.txtPassNew02.EditValue = "";
            this.txtPassNew02.Location = new System.Drawing.Point(161, 191);
            this.txtPassNew02.Name = "txtPassNew02";
            this.txtPassNew02.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassNew02.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassNew02.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassNew02.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassNew02.Properties.Appearance.Options.UseFont = true;
            this.txtPassNew02.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassNew02.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassNew02.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassNew02.Properties.PasswordChar = '*';
            this.txtPassNew02.Size = new System.Drawing.Size(193, 22);
            this.txtPassNew02.TabIndex = 3;
            this.txtPassNew02.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(32, 194);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(123, 16);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "Retype Password :";
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 259);
            this.Controls.Add(this.grpLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.FrmArac_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUser_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogin)).EndInit();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew01.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew02.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpLogin;
        private DevExpress.XtraEditors.TextEdit txtPassNew01;
        private DevExpress.XtraEditors.LabelControl lbPassNew;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lbPassOld;
        private DevExpress.XtraEditors.TextEdit txtPassOld;
        private DevExpress.XtraEditors.LabelControl lbUser;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtPassNew02;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}