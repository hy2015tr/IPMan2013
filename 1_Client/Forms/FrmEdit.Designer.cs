namespace IPMAN2013
{
    partial class FrmEdit
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
            this.propGrid = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.pageRow = new DevExpress.XtraTab.XtraTabPage();
            this.pageLog = new DevExpress.XtraTab.XtraTabPage();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.txtComment = new DevExpress.XtraEditors.TextEdit();
            this.lbComment = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.propGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.pageRow.SuspendLayout();
            this.pageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // propGrid
            // 
            this.propGrid.Appearance.Category.BackColor = System.Drawing.Color.DimGray;
            this.propGrid.Appearance.Category.BorderColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.Category.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.propGrid.Appearance.Category.ForeColor = System.Drawing.Color.White;
            this.propGrid.Appearance.Category.Options.UseBackColor = true;
            this.propGrid.Appearance.Category.Options.UseBorderColor = true;
            this.propGrid.Appearance.Category.Options.UseFont = true;
            this.propGrid.Appearance.Category.Options.UseForeColor = true;
            this.propGrid.Appearance.CategoryExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.propGrid.Appearance.CategoryExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.propGrid.Appearance.CategoryExpandButton.ForeColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.CategoryExpandButton.Options.UseBackColor = true;
            this.propGrid.Appearance.CategoryExpandButton.Options.UseBorderColor = true;
            this.propGrid.Appearance.CategoryExpandButton.Options.UseForeColor = true;
            this.propGrid.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.propGrid.Appearance.Empty.Options.UseBackColor = true;
            this.propGrid.Appearance.ExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.propGrid.Appearance.ExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.propGrid.Appearance.ExpandButton.ForeColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.ExpandButton.Options.UseBackColor = true;
            this.propGrid.Appearance.ExpandButton.Options.UseBorderColor = true;
            this.propGrid.Appearance.ExpandButton.Options.UseForeColor = true;
            this.propGrid.Appearance.FocusedCell.BackColor = System.Drawing.Color.Yellow;
            this.propGrid.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propGrid.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.FocusedCell.Options.UseBackColor = true;
            this.propGrid.Appearance.FocusedCell.Options.UseFont = true;
            this.propGrid.Appearance.FocusedCell.Options.UseForeColor = true;
            this.propGrid.Appearance.FocusedRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propGrid.Appearance.FocusedRecord.Options.UseBackColor = true;
            this.propGrid.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.propGrid.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Black;
            this.propGrid.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.propGrid.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.propGrid.Appearance.FocusedRow.Options.UseBackColor = true;
            this.propGrid.Appearance.FocusedRow.Options.UseForeColor = true;
            this.propGrid.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray;
            this.propGrid.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.propGrid.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.propGrid.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.propGrid.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.HorzLine.Options.UseBackColor = true;
            this.propGrid.Appearance.RecordValue.BackColor = System.Drawing.Color.White;
            this.propGrid.Appearance.RecordValue.ForeColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.RecordValue.Options.UseBackColor = true;
            this.propGrid.Appearance.RecordValue.Options.UseForeColor = true;
            this.propGrid.Appearance.RowHeaderPanel.BackColor = System.Drawing.Color.Gray;
            this.propGrid.Appearance.RowHeaderPanel.ForeColor = System.Drawing.Color.White;
            this.propGrid.Appearance.RowHeaderPanel.Options.UseBackColor = true;
            this.propGrid.Appearance.RowHeaderPanel.Options.UseForeColor = true;
            this.propGrid.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.propGrid.Appearance.VertLine.Options.UseBackColor = true;
            this.propGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGrid.Location = new System.Drawing.Point(0, 0);
            this.propGrid.Name = "propGrid";
            this.propGrid.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.propGrid.OptionsBehavior.ResizeHeaderPanel = false;
            this.propGrid.OptionsBehavior.ResizeRowHeaders = false;
            this.propGrid.OptionsBehavior.ResizeRowValues = false;
            this.propGrid.OptionsBehavior.UseEnterAsTab = true;
            this.propGrid.OptionsView.ShowRootCategories = false;
            this.propGrid.RecordWidth = 150;
            this.propGrid.RowHeaderWidth = 50;
            this.propGrid.Size = new System.Drawing.Size(634, 235);
            this.propGrid.TabIndex = 0;
            this.propGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.propGrid_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(459, 413);
            this.btnSave.LookAndFeel.SkinName = "Glass Oceans";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Tag = "ActionButton";
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(575, 413);
            this.btnCancel.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.EditValue = "TEST";
            this.txtTitle.Location = new System.Drawing.Point(21, 30);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtTitle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTitle.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtTitle.Properties.Appearance.Options.UseBackColor = true;
            this.txtTitle.Properties.Appearance.Options.UseFont = true;
            this.txtTitle.Properties.Appearance.Options.UseForeColor = true;
            this.txtTitle.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTitle.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTitle.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTitle.Properties.MaxLength = 10;
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(640, 32);
            this.txtTitle.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabControl.AppearancePage.Header.Options.UseFont = true;
            this.tabControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.tabControl.Location = new System.Drawing.Point(21, 78);
            this.tabControl.LookAndFeel.SkinName = "McSkin";
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.pageRow;
            this.tabControl.Size = new System.Drawing.Size(640, 264);
            this.tabControl.TabIndex = 4;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageRow,
            this.pageLog});
            // 
            // pageRow
            // 
            this.pageRow.Controls.Add(this.propGrid);
            this.pageRow.Name = "pageRow";
            this.pageRow.Size = new System.Drawing.Size(634, 235);
            this.pageRow.Text = "        Record        ";
            // 
            // pageLog
            // 
            this.pageLog.Controls.Add(this.txtLog);
            this.pageLog.Name = "pageLog";
            this.pageLog.Size = new System.Drawing.Size(634, 235);
            this.pageLog.Text = "        Log        ";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtLog.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLog.Properties.Appearance.Options.UseBackColor = true;
            this.txtLog.Properties.Appearance.Options.UseFont = true;
            this.txtLog.Properties.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(634, 235);
            this.txtLog.TabIndex = 0;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(95, 374);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtComment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtComment.Properties.Appearance.Options.UseBackColor = true;
            this.txtComment.Properties.Appearance.Options.UseFont = true;
            this.txtComment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtComment.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtComment.Size = new System.Drawing.Size(560, 20);
            this.txtComment.TabIndex = 34;
            this.txtComment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.propGrid_KeyUp);
            // 
            // lbComment
            // 
            this.lbComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbComment.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbComment.Location = new System.Drawing.Point(22, 376);
            this.lbComment.Name = "lbComment";
            this.lbComment.Size = new System.Drawing.Size(67, 14);
            this.lbComment.TabIndex = 35;
            this.lbComment.Text = "Comment :";
            // 
            // FrmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.lbComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Request Form";
            this.Shown += new System.EventHandler(this.FrmEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEdit_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.propGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.pageRow.ResumeLayout(false);
            this.pageLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propGrid;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage pageRow;
        private DevExpress.XtraTab.XtraTabPage pageLog;
        private DevExpress.XtraEditors.TextEdit txtComment;
        private DevExpress.XtraEditors.LabelControl lbComment;
        private DevExpress.XtraEditors.MemoEdit txtLog;
    }
}