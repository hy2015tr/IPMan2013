namespace IPMAN2013
{
    partial class FrmOnay
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
            this.grpDatabase = new DevExpress.XtraEditors.GroupControl();
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            this.radioRequest = new DevExpress.XtraEditors.RadioGroup();
            this.grpRequest = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).BeginInit();
            this.grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRequest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRequest)).BeginInit();
            this.grpRequest.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatabase
            // 
            this.grpDatabase.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpDatabase.AppearanceCaption.Options.UseFont = true;
            this.grpDatabase.Controls.Add(this.txtComment);
            this.grpDatabase.Location = new System.Drawing.Point(14, 262);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(460, 130);
            this.grpDatabase.TabIndex = 4;
            this.grpDatabase.Text = "Comment";
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(2, 21);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtComment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtComment.Properties.Appearance.Options.UseBackColor = true;
            this.txtComment.Properties.Appearance.Options.UseFont = true;
            this.txtComment.Size = new System.Drawing.Size(456, 107);
            this.txtComment.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(365, 414);
            this.btnCancel.LookAndFeel.SkinName = "Stardust";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(239, 412);
            this.btnSave.LookAndFeel.SkinName = "Glass Oceans";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Tag = "ActionButton";
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbTitle.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.lbTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(504, 45);
            this.lbTitle.TabIndex = 64;
            this.lbTitle.Text = "PBN Confirmation";
            // 
            // radioRequest
            // 
            this.radioRequest.EditValue = "";
            this.radioRequest.Location = new System.Drawing.Point(30, 46);
            this.radioRequest.Name = "radioRequest";
            this.radioRequest.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.radioRequest.Properties.Appearance.Options.UseFont = true;
            this.radioRequest.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("APPROVED", "  ( 1 )  APPROVE"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("REJECTED", "  ( 2 )  REJECT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("WAITING", "  ( 3 )  ROLLBACK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CANCELED", "  ( 4 )  CANCEL")});
            this.radioRequest.Size = new System.Drawing.Size(400, 100);
            this.radioRequest.TabIndex = 68;
            this.radioRequest.SelectedIndexChanged += new System.EventHandler(this.radioRequest_SelectedIndexChanged);
            // 
            // grpRequest
            // 
            this.grpRequest.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpRequest.AppearanceCaption.Options.UseFont = true;
            this.grpRequest.Controls.Add(this.radioRequest);
            this.grpRequest.Location = new System.Drawing.Point(14, 69);
            this.grpRequest.Name = "grpRequest";
            this.grpRequest.Size = new System.Drawing.Size(460, 173);
            this.grpRequest.TabIndex = 69;
            this.grpRequest.Text = "Request Status";
            // 
            // FrmOnay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 462);
            this.Controls.Add(this.grpRequest);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpDatabase);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOnay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approval Form";
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).EndInit();
            this.grpDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRequest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRequest)).EndInit();
            this.grpRequest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpDatabase;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraEditors.RadioGroup radioRequest;
        private DevExpress.XtraEditors.GroupControl grpRequest;
        public DevExpress.XtraEditors.MemoEdit txtComment;

    }
}