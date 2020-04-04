using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Data;
using System.Drawing;
using DevExpress.XtraTab;
using System.Collections;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraCharts;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;



namespace IPMAN2013
{
    public partial class FrmMain : XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        #region //---Member Fields---//

        // Splash Form
        private static FrmSplash m_frmSplash = new FrmSplash();

        // SessionInfo
        private alfaSession m_SessionInfo = new alfaSession();

        // Active Grid
        private GridView m_grdViewActive = null;

        // WAPI Object
        private alfaWAPI m_WAPI = null;

        #endregion

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmMain()
        {
            // Show Splash
            m_frmSplash.Show();
            m_frmSplash.Update();

            // Initializing
            this.InitializeComponent();

            // Hide pnMain
            pnMain.Visible = false;

            // Maximized
            this.WindowState = FormWindowState.Maximized;

            // Set LookAndFeel
            this.SetDefaultLook(menuSkinOffice2010Black);

            // Set Version
            this.lbVersion.Text = alfaStr.GetAppVersion(true);

            // Select Test-Prod
            radioSystem.SelectedIndex = 0;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                // Update
                this.Update();

                // Close Splash
                m_frmSplash.Close();

                // Clear 
                this.btnLoginClear_Click(null, null);

                // Load UserName
                this.LoadUserNameFromAppSettings();

                // Tables
                this.Admin_Tables_Items();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SetDefaultLook(DevExpress.XtraBars.BarButtonItem p_MenuItem)
        {
            // Add Check to ViewMenuItems
            foreach (Object Item in barMenu.Manager.Items)
            {
                // Check Item
                if (Item.GetType().ToString() != "DevExpress.XtraBars.BarButtonItem") continue;

                // Get Item
                BarButtonItem obj = (BarButtonItem)Item;

                if (obj.Name.Contains("Skin"))
                {
                    // Set Properties
                    obj.ButtonStyle = BarButtonStyle.Check;
                    obj.AllowAllUp = true;
                    obj.GroupIndex = 1;
                }
            }

            // Set Default LookAndFeel
            defaultLookAndFeel.LookAndFeel.SkinName = p_MenuItem.Caption;
            p_MenuItem.Down = true;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Show Splash
            m_frmSplash = new FrmSplash();
            m_frmSplash.ShowDialog();

            //Hide Splash
            m_frmSplash.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void ViewItemALL_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Set LookAndFeel
            defaultLookAndFeel.LookAndFeel.SkinName = e.Item.Caption;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLoginClear_Click(object sender, EventArgs e)
        {
            // Disable Login
            alfaCtrl.DisableControl(btnLogin);

            // Clear Inputs
            txtUser.Text = string.Empty;
            txtPass.Text = string.Empty;

            // Focus
            txtUser.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if ((txtUser.Text != string.Empty) && (txtPass.Text != string.Empty))
            {
                // Enable Login
                alfaCtrl.EnableControl(btnLogin);
            }
            else
            {
                // Disable Login
                alfaCtrl.DisableControl(btnLogin);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && btnLogin.Enabled == true)
            {
                // Enter
                this.btnLogin_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (alfaEntity.CheckUserLogin(txtUser.Text, txtPass.Text, ref this.m_SessionInfo))
                {
                    // Refresh ClientInfo
                    this.m_SessionInfo.User = txtUser.Text;
                    this.m_SessionInfo.RefreshLoginDateTime();

                    // Save UserName
                    IPMAN2013.Properties.Settings.Default.UserName = txtUser.Text;
                    IPMAN2013.Properties.Settings.Default.Save();

                    // Load Variants
                    this.GridView_Variant(true);

                    // Set Admin Page
                    pageAdmin.PageVisible = (this.m_SessionInfo.Admin);
                    pageSession.PageVisible = (this.m_SessionInfo.Admin);

                    // Set Report Page
                    pageChart.PageVisible = true;

                    // Set PBN Buttons
                    alfaCtrl.SetButton(btnRequestPBN, false);
                    alfaCtrl.SetButton(btnRequestWAPI, false);

                    // Create WAPI
                    this.m_WAPI = new alfaWAPI(this.statusResult);

                    // Set Status
                    this.Set_Status_Fields();

                    // Clear Buttons
                    this.btnSubnetClear_Click(null, null);
                    this.btnIPv4Clear_Click(null, null);
                    this.btnRequestClear_Click(null, null);
                    this.txtIPv4Network_KeyUp(null, null);
                    this.menu3Comment_ItemClick(null, null);
                    this.btnChartClear_Click(null, null);
                    this.btnSessionClear_Click(null, null);

                    // Add Session
                    alfaEntity.TableSession_Add(this.m_SessionInfo);

                    // Panels
                    pnLogin.Hide();
                    pnMain.Show();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void LoadUserNameFromAppSettings()
        {
            // Load UserName
            string p_Username = IPMAN2013.Properties.Settings.Default.UserName;

            if (p_Username != string.Empty)
            {
                // Set UserName
                txtUser.Text = p_Username;

                // Focus Password
                txtPass.Focus();
            }
            // Focus UserName
            else txtUser.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_Status_Fields()
        {
            try
            {
                // Satus Left Items
                statusName.Caption = String.Format("PC : {0}", this.m_SessionInfo.PC);
                statusIP.Caption = String.Format("IP : {0}", this.m_SessionInfo.LocIP);
                statusNet.Caption = String.Format("NET : {0}", this.m_SessionInfo.NetVer);
                statusSQL.Caption = String.Format("SQL : {0}", this.m_SessionInfo.DBName);

                // Set Server
                statusSRV.Caption = this.m_WAPI.GetAddress();

                // Set User
                statusUser.Caption = this.m_SessionInfo.UserGroup();

                // Set Version
                this.lbVersion.Text = this.m_SessionInfo.AppVer;
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Update Record
                this.Admin_Tables_Update();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // Add Record
                this.Admin_Tables_Insert();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete Record
                this.Admin_Tables_Delete();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Items()
        {
            // Clear List
            listAdminTables.Items.Clear();

            // Add Items
            listAdminTables.Items.Add(new alfaItem(" ( 1 )  -  USERS     ", "TableUser"));
            listAdminTables.Items.Add(new alfaItem(" ( 2 )  -  GROUPS    ", "TableGroup"));
            listAdminTables.Items.Add(new alfaItem(" ( 3 )  -  SUBNETS   ", "TableSubnet"));
            listAdminTables.Items.Add(new alfaItem(" ( 4 )  -  SERVERS   ", "TableServer"));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Refresh()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get Item
                var p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Clear
                grdAdminView.Columns.Clear();

                // Set Datasource
                switch (p_Item.Name)
                {
                    case "TableUser": grdAdmin.DataSource = alfaEntity.TableUser_GetList(null, null); break;
                    case "TableGroup": grdAdmin.DataSource = alfaEntity.TableGroup_GetList(null, null); break;
                    case "TableSubnet": grdAdmin.DataSource = alfaEntity.TableSubnet_GetList(null, null); break;
                    case "TableServer": grdAdmin.DataSource = alfaEntity.TableServer_GetList(null, null); break;
                }

                // Set GridView
                alfaGrid.SetView(grdAdminView, true);

                // Set Buttons
                alfaCtrl.SetButton(btnAdminUpdate, (grdAdminView.RowCount > 0));
                alfaCtrl.SetButton(btnAdminDelete, (grdAdminView.RowCount > 0));
                alfaCtrl.EnableControl(btnAdminInsert);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Update()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Object
                Object objEntity = null;

                // Check
                if (grdAdminView.FocusedRowHandle < 0) return;

                // Get ID
                int p_ID = (int)grdAdminView.GetRowCellValue(grdAdminView.FocusedRowHandle, "ID");

                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Assign Object
                switch (p_Item.Name)
                {
                    case "TableGroup": objEntity = DS.Context.TableGroup.First(tt => tt.ID == p_ID); break;
                    case "TableServer": objEntity = DS.Context.TableServer.First(tt => tt.ID == p_ID); break;
                    case "TableSubnet": objEntity = DS.Context.TableSubnet.First(tt => tt.ID == p_ID); break;
                    case "TableUser": objEntity = DS.Context.TableUser.First(tt => tt.ID == p_ID); break;
                }

                // Create Form
                FrmRecord frm = new FrmRecord(p_Item.ToString(), objEntity);

                // Confirmation
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // SaveChanges
                    DS.Context.SaveChanges();

                    // Refresh
                    this.Admin_Tables_Refresh();

                    // Get Row
                    int p_RowHandle = grdAdminView.LocateByValue("ID", p_ID, null);

                    // Select Row
                    alfaGrid.SelectRow(grdAdminView, p_RowHandle);

                    // Update Status
                    statusSRV.Caption = this.m_WAPI.GetAddress();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Insert()
        {
            using (alfaDS ent = new alfaDS())
            {
                // Create Object
                Object objEntity = null;

                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Assign Object
                switch (p_Item.Name)
                {
                    case "TableGroup": objEntity = new TableGroup(); break;
                    case "TableServer": objEntity = new TableServer(); break;
                    case "TableSubnet": objEntity = new TableSubnet(); break;
                    case "TableUser": objEntity = new TableUser(); break;
                }

                // Create Form
                FrmRecord frm = new FrmRecord(p_Item.ToString(), objEntity);

                // Confirmation
                if (frm.ShowDialog() != DialogResult.OK) return;

                // Add Record
                switch (p_Item.Name)
                {
                    case "TableGroup": ent.Context.TableGroup.Add(objEntity as TableGroup); break;
                    case "TableServer": ent.Context.TableServer.Add(objEntity as TableServer); break;
                    case "TableSubnet": ent.Context.TableSubnet.Add(objEntity as TableSubnet); break;
                    case "TableUser": ent.Context.TableUser.Add(objEntity as TableUser); break;
                }

                // SaveChanges
                ent.Context.SaveChanges();
            }

            // DataSource
            this.Admin_Tables_Refresh();

            // Select Row
            alfaGrid.SelectRow(grdAdminView, grdAdminView.RowCount - 1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Delete()
        {
            // Confirmation
            if (alfaMsg.Quest("Are You Sure to Delete the Selected Record ?") == DialogResult.No) return;

            using (alfaDS ent = new alfaDS())
            {
                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Get ID
                int p_ID = (int)grdAdminView.GetRowCellValue(grdAdminView.FocusedRowHandle, "ID");

                // Delete Object
                switch (p_Item.Name)
                {
                    case "TableGroup": ent.Context.TableGroup.Remove(ent.Context.TableGroup.First(tt => tt.ID == p_ID)); break;
                    case "TableServer": ent.Context.TableServer.Remove(ent.Context.TableServer.First(tt => tt.ID == p_ID)); break;
                    case "TableSubnet": ent.Context.TableSubnet.Remove(ent.Context.TableSubnet.First(tt => tt.ID == p_ID)); break;
                    case "TableUser": ent.Context.TableUser.Remove(ent.Context.TableUser.First(tt => tt.ID == p_ID)); break;
                }

                // SaveChanges
                ent.Context.SaveChanges();
            }

            // DataSource
            this.Admin_Tables_Refresh();

            //Focus
            grdAdminView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void listAdminTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTabPage == pageAdmin)
            {
                // Get Tables
                this.Admin_Tables_Refresh();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSubnetList_Click(object sender, EventArgs e)
        {
            // Check
            if (this.m_WAPI.IsRunning) return;

            // Disable Controls
            alfaCtrl.DisableButtons(grpSubnet);
            alfaCtrl.DisablePages(tabMain, pageSubnet);

            // Set Control
            MainTimer.Tag = grpSubnet;

            // Start Timer
            MainTimer.Start();

            // Get List
            this.m_WAPI.GetSubnetList(grdSubnetView, txtSubnetName.Text, txtSubnetCount.Text, txtNetwork.Text);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIPv4List_Click(object sender, EventArgs e)
        {
            // Check
            if (this.m_WAPI.IsRunning) return;

            // Disable Controls
            alfaCtrl.DisableButtons(grpIPv4List);
            alfaCtrl.DisablePages(tabMain, pageIPv4);

            // Set Control
            MainTimer.Tag = grpIPv4List;

            // Start Timer
            MainTimer.Start();

            // Get List
            this.m_WAPI.GetIPv4List(grdIPv4View, txtIPv4Network.Text, txtIPv4Count.Text, txtIPv4Status.Text);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIPv4Clear_Click(object sender, EventArgs e)
        {
            // Reset Progress
            statusProgress.EditValue = 0;

            // Reset Values
            txtIPv4Count.SelectedIndex = 0;
            txtIPv4Status.EditValue = alfaStr.UNUSED;

            // Disable Buttons
            alfaCtrl.DisableControl(btnIPv4Clear);
            alfaCtrl.DisableControl(btnIPv4Request);
            alfaCtrl.DisableControl(btnIPv4Reclaim);
            alfaCtrl.DisableControl(btnIPv4Resolve);

            // Reset Grid
            grdIPv4View.Columns.Clear();
            grdIPv4View.GridControl.DataSource = null;

            // Set Message
            alfaCtrl.SetText(this.statusResult, "WAPI : OK", true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSubnetClear_Click(object sender, EventArgs e)
        {
            // Reset Progress
            statusProgress.EditValue = 0;

            // Add LookUp
            alfaEntity.TableSubnet_ComboBox(txtSubnetName);

            // Reset Values
            txtNetwork.Text = string.Empty;
            txtSubnetName.SelectedIndex = 0;
            txtSubnetCount.SelectedIndex = 0;

            // Reset Grid
            grdSubnetView.Columns.Clear();
            grdSubnetView.GridControl.DataSource = null;

            // Set Message
            alfaCtrl.SetText(this.statusResult, "WAPI : OK", true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            // Get Values
            int p_Value = (int)statusProgress.EditValue;

            // Increase Value
            p_Value = p_Value + 10;

            // Take Mode
            p_Value = p_Value % 100;

            // Set Value
            statusProgress.EditValue = p_Value;

            if (this.m_WAPI.IsRunning == false)
            {
                // Stop Timer
                this.MainTimer.Stop();

                // ProgressBar
                statusProgress.EditValue = 100;

                // Enable Controls
                alfaCtrl.EnablePages(tabMain);
                alfaCtrl.EnableButtons((Control)MainTimer.Tag);

                // Refresh
                if (MainTimer.Tag == grpRequest) this.btnRequestList_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdSubnetView_DoubleClick(object sender, EventArgs e)
        {
            if (grdSubnetView.RowCount > 0)
            {
                // Check
                if (grdSubnetView.IsGroupRow(grdSubnetView.FocusedRowHandle)) return;

                // Check
                if (grdSubnetView.FocusedColumn.FieldName != "Network") return;

                // Get Lisy
                var list = (List<rowSubnet>)grdSubnetView.DataSource;

                // Uncheck All Items
                foreach (var tt in list) tt.Check = false;

                // Refresh 
                grdSubnetView.RefreshData();

                // Get Row
                var row = (rowSubnet)grdSubnetView.GetFocusedRow();

                // Set Flag
                row.Check = true;

                // Set Page
                tabMain.SelectedTabPage = pageIPv4;

                // Clear
                this.btnIPv4Clear_Click(null, null);

                // Set IP Address
                txtIPv4Network.Text = row.Network;

                // Call List
                this.btnIPv4List_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtIPv4Network_KeyUp(object sender, KeyEventArgs e)
        {
            // Set Button List
            if (txtIPv4Network.Text.Trim().Length > 0)
                alfaCtrl.EnableControl(btnIPv4List);
            else alfaCtrl.DisableControl(btnIPv4List);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == pageAdmin)
            {
                // Set ListBox Index
                this.listAdminTables_SelectedIndexChanged(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAdminView_DoubleClick(object sender, EventArgs e)
        {
            // Update
            this.btnAdminUpdate_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAdminView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                // Update 
                if (e.KeyCode == Keys.Enter && btnAdminUpdate.Enabled) this.btnAdminUpdate_Click(null, null);

                // Delete
                if (e.KeyCode == Keys.Delete && btnAdminDelete.Enabled) this.btnAdminDelete_Click(null, null);

                // Insert
                if (e.KeyCode == Keys.Insert && btnAdminInsert.Enabled) this.btnAdminInsert_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusSQL_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Create Form
            FrmSqlServer frm = new FrmSqlServer(this.m_SessionInfo);

            // Call Form
            if (frm.ShowDialog() == DialogResult.OK)
            {
                statusSQL.Caption = String.Format("SQL : {0}", this.m_SessionInfo.DBName);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdALLView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            // Check
            if (e.RowHandle < 0) return;

            // Get Grid
            GridView p_GridView = (GridView)sender;

            // Get Check
            bool p_Check = Convert.ToBoolean(p_GridView.GetRowCellValue(e.RowHandle, "Check"));

            // Selected Row
            if (e.RowHandle == p_GridView.FocusedRowHandle)
            {
                // Set Font
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }

            else if ("Network,IPAddress".Contains(e.Column.FieldName))
            {
                if (!p_Check && (string)p_GridView.GetRowCellValue(e.RowHandle, "ReqStatus") != alfaStr.PROCESSING)
                {
                    // Custom View
                    e.Appearance.BackColor = Color.Lavender;
                    e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Regular);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdIPv4View_ShowingEditor(object sender, CancelEventArgs e)
        {
            // Get Grid
            GridView grdView = sender as GridView;

            // Row
            var row = (rowIPv4)grdIPv4View.GetFocusedRow();

            // Check
            if (row.ReqStatus == alfaStr.PROCESSING)
            {
                // Disable All
                e.Cancel = true;
            }

            // Enable Edit for "Check"
            else if (grdView.FocusedColumn.FieldName == "Check")
            {
                // Enable
                e.Cancel = false;

                // Set SiteName
                if (row.ExtSite == string.Empty) row.ExtSite = txtSubnetName.Text;
            }

            else if (grdView.FocusedColumn.FieldName == "ExtVM")
            {
                if (!row.ExtVM)
                {
                    // Set Values
                    row.ExtBackup = false;
                    row.ExtSwitch1 = string.Empty;
                    row.ExtSwitch2 = string.Empty;
                    row.ExtModulePort1 = string.Empty;
                    row.ExtModulePort2 = string.Empty;
                }
            }

            else if (grdView.FocusedColumn.FieldName == "ExtBackup")
            {
                if (row.ExtVM)
                {
                    // Disable
                    e.Cancel = true;

                    // Set Values
                    row.ExtBackup = false;
                    row.ExtSwitch1 = string.Empty;
                    row.ExtSwitch2 = string.Empty;
                    row.ExtModulePort1 = string.Empty;
                    row.ExtModulePort2 = string.Empty;
                }
            }

            // << IT >>
            else if (this.m_SessionInfo.Group == alfaStr.IT && "ExtPriority-ExtServerName-ExtResponsible-ExtNote-ExtVM-ExtBackup-ExtPortSecurity".Contains(grdView.FocusedColumn.FieldName))
            {
                if (row.Check == true) e.Cancel = false; else e.Cancel = true;
            }

            // << CABLING >>
            else if (this.m_SessionInfo.Group == alfaStr.CABLING && "ExtSwitch1-ExtSwitch2-ExtModulePort1-ExtModulePort2-ExtNote".Contains(grdView.FocusedColumn.FieldName))
            {
                if (row.Check == true) e.Cancel = false; else e.Cancel = true;
            }

            // << PBN >>
            else if (this.m_SessionInfo.Group == alfaStr.PBN && grdView.FocusedColumn.FieldName.Contains("Ext"))
            {
                if (row.Check == true) e.Cancel = false; else e.Cancel = true;
            }

            else
            {
                // Disable Rest
                e.Cancel = true;
            }

            // Refresh
            grdView.RefreshRow(grdView.FocusedRowHandle);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdSubnetView_ShowingEditor(object sender, CancelEventArgs e)
        {
            // Disable
            e.Cancel = true;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdALLView_RowStyle(object sender, RowStyleEventArgs e)
        {
            // GridView
            GridView p_GridView = sender as GridView;

            // Check
            if (e.RowHandle < 0) return;

            // HighPriority
            e.HighPriority = true;

            // Get Check
            bool p_Check = Convert.ToBoolean(p_GridView.GetRowCellValue(e.RowHandle, "Check"));

            if (p_Check == true)
            {
                // Selected Item
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.RoyalBlue;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }
            else if (e.RowHandle == p_GridView.FocusedRowHandle)
            {
                // Focused Item
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.RoyalBlue;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }

            else if ((string)p_GridView.GetRowCellValue(e.RowHandle, "ReqStatus") == alfaStr.PROCESSING)
            {
                // Processing Item
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.LightSalmon;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnRequestClear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // Clear Log
            txtLog.Text = string.Empty;

            // Reset Values
            txtRequestType.SelectedIndex = 0;
            txtRequestLevel.SelectedIndex = 0;
            txtRequestDate01.DateTime = dtNow.AddDays(-3);
            txtRequestDate02.DateTime = dtNow.AddDays(+3);

            // Clear Grid
            grdRequestView.GridControl.DataSource = null;
            grdRequestView.ClearColumnsFilter();
            grdRequestView.Columns.Clear();

            // Focus
            grdRequestView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnRequestList_Click(object sender, EventArgs e)
        {
            // Get List            
            var ListReq = alfaEntity.TableRequest_GetList(txtRequestDate01.DateTime, txtRequestDate02.DateTime, txtRequestLevel.SelectedIndex, txtRequestType.Text);

            if (ListReq.Count > 0)
            {
                // Set PBN Buttons
                alfaCtrl.SetButton(btnRequestPBN, this.m_SessionInfo.Group == alfaStr.PBN && ( txtRequestLevel.SelectedIndex == 1 || txtRequestLevel.SelectedIndex == 2 || txtRequestLevel.SelectedIndex == 3 ));
                alfaCtrl.SetButton(btnRequestWAPI, this.m_SessionInfo.Group == alfaStr.PBN && txtRequestLevel.SelectedIndex == 3);
            }

            // Set GridView
            grdRequestView.GridControl.DataSource = ListReq;
            alfaGrid.SetView(grdRequestView, false);
            grpRequest.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIPv4Request_Click(object sender, EventArgs e)
        {
            // Get List
            var listReq = (List<rowIPv4>)grdIPv4.DataSource;

            // Check Selected Items
            if ((from tt in listReq where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            // Check for Responsible
            if ((from tt in listReq where string.IsNullOrEmpty(tt.ExtResponsible) && tt.IPStatus == alfaStr.UNUSED && tt.Check == true select tt).Count() > 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  For New IPs, Responsible Field must be Filled Out ... !"); return;
            }

            // Confirmation
            if (alfaMsg.Quest("Are You Sure to Reserve the Selected IPs ?") != DialogResult.Yes) return;

            try
            {
                // Cursor
                alfaMsg.CursorWait();

                // IP List
                string p_ListIP = null;

                // TaskID
                int p_TaskID = alfaEntity.GetNextTaskID();

                foreach (var row in listReq)
                {
                    if (row.Check == true)
                    {
                        // Create ReqType
                        string p_ReqType = string.Empty;

                        // Set ReqType
                        if (row.IPStatus == alfaStr.UNUSED) p_ReqType = alfaStr.IP_NEW; else p_ReqType = alfaStr.IP_EDIT;

                        // Add Request to Database
                        if (alfaEntity.TableRequest_Add(row, this.m_SessionInfo, p_TaskID, p_ReqType))
                        {
                            // Add List
                            p_ListIP += row.IPAddress + "(" + row.ExtServerName + ") -- ";
                        }
                    }
                }

                // Set Message
                this.m_SessionInfo.ReqStatus = "New Request(s) for Approval";

                // Create Body
                string p_MailBody = alfaMail.CreateBody(p_TaskID.ToString(), p_ListIP, this.m_SessionInfo, "PBN Approval", string.Empty);

                // Send Mail to PBN Group
                alfaMail.Send(alfaEntity.TableGroup_Get("PBN").Email, this.m_SessionInfo.Email, "IPMAN2013 - New Request(s) for Approval", p_MailBody);

                // Message
                alfaMsg.Info("Reservation Process is Done ... !");

                // Refresh
                this.btnIPv4List_Click(null, null);
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIPv4Reclaim_Click(object sender, EventArgs e)
        {
            // Get List
            var listReq = (List<rowIPv4>)grdIPv4.DataSource;

            // Check Selected Items
            if ((from tt in listReq where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            // Confirmation
            if (alfaMsg.Quest("Are You Sure to Reclaim the Selected IPs ?") != DialogResult.Yes) return;

            try
            {
                // Cursor
                alfaMsg.CursorWait();

                // IP List
                string p_ListIP = null;

                // TaskID
                int p_TaskID = alfaEntity.GetNextTaskID();

                foreach (var row in listReq)
                {
                    if (row.Check == true)
                    {
                        // Add Request to Database
                        alfaEntity.TableRequest_Add(row, this.m_SessionInfo, p_TaskID, alfaStr.IP_DEL);

                        // Add List
                        p_ListIP += row.IPAddress + "(" + row.ExtServerName + ") -- ";
                    }
                }

                // Set Message
                this.m_SessionInfo.ReqStatus = "New Request(s) for Approval";

                // Create Body
                string p_MailBody = alfaMail.CreateBody(p_TaskID.ToString(), p_ListIP, this.m_SessionInfo, "PBN Approval", string.Empty);

                // Send Mail to PBN Group
                alfaMail.Send(alfaEntity.TableGroup_Get("PBN").Email, this.m_SessionInfo.Email, "IPMAN2013 - New Request(s) for Approval", p_MailBody);

                // Message
                alfaMsg.Info("Request(s) Created Successfully for ReClaim ... !");

                // Refresh
                this.btnIPv4List_Click(null, null);
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIPv4Resolve_Click(object sender, EventArgs e)
        {
            // Get List
            var listReq = (List<rowIPv4>)grdIPv4.DataSource;

            // Get Selected
            var listSelected = from tt in listReq where tt.Check == true && tt.IPStatus == alfaStr.UNUSED && tt.PingStatus == alfaStr.PING_REPLIED select tt;

            // Check Selected Items
            if (listSelected.Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select Correct Item(s) ... !"); return;
            }

            // Confirmation
            if (alfaMsg.Quest("Are You Sure to Resolve the Selected IPs ?") != DialogResult.Yes) return;

            try
            {
                // Cursor
                alfaMsg.CursorWait();

                // TaskID
                int p_TaskID = alfaEntity.GetNextTaskID();

                foreach (var row in listSelected)
                {
                    // Set Properties
                    row.ExtSite = txtSubnetName.Text;
                    row.ExtModulePort1 = "PBN";
                    row.ExtModulePort2 = "PBN";
                    row.ExtResponsible = "PBN";
                    row.ExtServerName = "PBN";
                    row.ExtSwitch1 = "PBN";
                    row.ExtSwitch2 = "PBN";
                    row.ExtVlanNo = "PBN";
                    row.ExtNote = "PBN";
                    
                    // Add Request to Database
                    alfaEntity.TableRequest_Add(row, this.m_SessionInfo, p_TaskID, alfaStr.IP_NEW);

                    // Update Request
                    alfaEntity.TableRequest_Update(p_TaskID, this.m_SessionInfo);
                }

                // Message
                alfaMsg.Info("Request(s) Created Successfully for ReSolve ... !");

                // Refresh
                this.btnIPv4List_Click(null, null);

            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdRequestView_ShowingEditor(object sender, CancelEventArgs e)
        {
            // Row
            var row = (TableRequest)grdRequestView.GetFocusedRow();

            // Enable Edit for "Check"
            if (grdRequestView.FocusedColumn.FieldName == "Check")
            {
                // Enable
                e.Cancel = false;
            }

            // else
            else e.Cancel = true;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdALL_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            // GridView
            GridView p_GridView = sender as GridView;

            // Check
            if (e.RowHandle < 0) return;

            // Check Focused Row
            if (e.RowHandle == p_GridView.FocusedRowHandle) return;

            // Check Selected Row
            if (p_GridView.GetRowCellValue(e.RowHandle, "Check") != null)
            {
                if ((bool)p_GridView.GetRowCellValue(e.RowHandle, "Check") == true) return;
            }

            if ("USR_Request_01/PBN_Apprv_02/CBL_Switch_03/PBN_Close_04/Ping/PingStatus".Contains(e.Column.FieldName))
            {
                // Get Text
                string p_CellValue = p_GridView.GetRowCellDisplayText(e.RowHandle, p_GridView.Columns[e.Column.FieldName]);

                // Set Fonts
                e.Appearance.ForeColor = Color.White;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);

                // Set Color
                if (p_CellValue == alfaStr.ERROR) e.Appearance.BackColor = Color.Red;
                else if (p_CellValue == alfaStr.OKEY) e.Appearance.BackColor = Color.Green;
                else if (p_CellValue == alfaStr.WAITING) e.Appearance.BackColor = Color.DarkGray;
                else if (p_CellValue == alfaStr.APPROVED) e.Appearance.BackColor = Color.Green;
                else if (p_CellValue == alfaStr.CANCELED) e.Appearance.BackColor = Color.Gray;
                else if (p_CellValue == alfaStr.REJECTED) e.Appearance.BackColor = Color.Red;
                else if (p_CellValue == alfaStr.PING_TIMEOUT) e.Appearance.BackColor = Color.Green;
                else if (p_CellValue == alfaStr.PING_REPLIED) e.Appearance.BackColor = Color.Maroon;
                else if (p_CellValue == alfaStr.CLOSED) e.Appearance.BackColor = Color.Green;
                else if (p_CellValue == alfaStr.ACTIVE) e.Appearance.BackColor = Color.Red;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnRequestPBN_Click(object sender, EventArgs e)
        {
            // Get List
            var listReq = (List<TableRequest>)grdRequestView.GridControl.DataSource;

            // Check Selected Items
            if ((from tt in listReq where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            //  List IP
            string p_ListIP = string.Empty;

            // List Email
            string p_ListEmail = string.Empty;

            // List TaskID
            string p_ListTaskID = string.Empty;

            foreach (var p_Req in listReq.Where(tt => tt.Check == true).ToList())
            {

                // Get Email
                string p_Email = alfaEntity.GetUserEmail(p_Req.ReqUser);
                
                // Add List Email
                if (!p_ListEmail.Contains(p_Email)) p_ListEmail += p_Email + ";";
                
                // Add List IP
                if (!p_ListIP.Contains(p_Req.IPAddress)) p_ListIP += p_Req.IPAddress + "(" + p_Req.ExtServerName + ") -- ";

                // Add List TaskID
                if(!p_ListTaskID.Contains(p_Req.TaskID.ToString())) p_ListTaskID += p_Req.TaskID.ToString() + " -- ";
            }

            // Create Form
            var frmOnay = new FrmOnay(this.m_SessionInfo, listReq);

            if (frmOnay.ShowDialog() == DialogResult.OK)
            {
                // Create Body
                string p_MailBody = alfaMail.CreateBody(p_ListTaskID, p_ListIP, this.m_SessionInfo, "IT Notification", frmOnay.txtComment.Text);

                // Send Mail ( ReqUsers + CABLING )
                alfaMail.Send(p_ListEmail + alfaEntity.TableGroup_Get("CABLING").Email, this.m_SessionInfo.Email, "IPMAN2013 - Request(s) " + this.m_SessionInfo.ReqStatus, p_MailBody);

                // Refresh
                this.btnRequestList_Click(null, null);

                // Message
                alfaMsg.Info("Request(s) were Successfully Processed ... !");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnRequestWAPI_Click(object sender, EventArgs e)
        {
            // Check
            if (this.m_WAPI.IsRunning) return;

            // Get List
            var listReq = (List<TableRequest>)grdRequestView.GridControl.DataSource;

            // Check Selected Items
            if ((from tt in listReq where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            // Get Checked Items
            var qry = from tt in listReq
                      where tt.Check == true
                         && tt.USR_Request_01 == alfaStr.OKEY
                         && tt.PBN_Apprv_02 == alfaStr.APPROVED
                         && tt.CBL_Switch_03 == alfaStr.OKEY
                         && tt.PBN_Close_04 != alfaStr.OKEY
                      select tt;

            // Check Count
            if (qry.ToList().Count ==0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select Item(s) Ready for InfoBlox ... !"); return;
            }

            // Confirmation
            if (alfaMsg.Quest("Are You Sure to Send InfoBlox the Selected Records ?") != DialogResult.Yes) return;

            // Disable Controls
            alfaCtrl.DisableButtons(grpRequest);
            alfaCtrl.DisablePages(tabMain, pageRequest);

            // Set Control
            MainTimer.Tag = grpRequest;

            // Start Timer
            MainTimer.Start();

            //  List IP
            string p_ListIP = string.Empty;

            // List Email
            string p_ListEmail = string.Empty;

            // List TaskID
            string p_ListTaskID = string.Empty;

            foreach (var p_Req in listReq.Where(tt => tt.Check == true).ToList())
            {
                // Get Email
                string p_Email = alfaEntity.GetUserEmail(p_Req.ReqUser);
                
                // Add List Email
                if (!p_ListEmail.Contains(p_Email)) p_ListEmail += p_Email + ";";
                
                // Add List IP
                if (!p_ListIP.Contains(p_Req.IPAddress)) p_ListIP += p_Req.IPAddress + "(" + p_Req.ExtServerName + ") -- ";

                // Add List TaskID
                if(!p_ListTaskID.Contains(p_Req.TaskID.ToString())) p_ListTaskID += p_Req.TaskID.ToString() + " -- ";
            }

            // Set Message
            this.m_SessionInfo.ReqStatus = "Request(s) were Sent to InfoBlox";

            // Create Body
            string p_MailBody = alfaMail.CreateBody(p_ListTaskID, p_ListIP, this.m_SessionInfo, "IT Notification",string.Empty);

            // Send Mail ( IT + CABLING )
            alfaMail.Send( p_ListEmail + alfaEntity.TableGroup_Get("CABLING").Email, this.m_SessionInfo.Email, "IPMAN2013 - Request(s) were Sent to InfoBlox", p_MailBody);

            // Send InfoBlox
            this.m_WAPI.UpdateIPv4List_SSH(qry.ToList(), this.m_SessionInfo);

            // Refresh
            this.btnRequestList_Click(null, null);

            // Message
            alfaMsg.Info("Request(s) were Sent to InfoBlox");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SelectItems(bool p_Status)
        {

            for (int li = 0; li < this.m_grdViewActive.RowCount; li++)
            {
                // Get Value
                var strValue = (string)m_grdViewActive.GetRowCellValue(li, "ReqStatus");

                if (strValue != alfaStr.PROCESSING)
                {
                    this.m_grdViewActive.SetRowCellValue(li, "Check", p_Status);
                }
            }

            // Refresh
            this.m_grdViewActive.RefreshEditor(true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdALLView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Check for Empty Records
                if ((sender as GridView).DataRowCount == 0) return;

                // Set Activator
                this.m_grdViewActive = sender as GridView;

                if (m_grdViewActive == grdRequestView)
                {
                    // Show Menu
                    menu4Comment.Visibility = BarItemVisibility.Always;
                    menu5Export.Visibility = BarItemVisibility.Always;
                    menu6Reset.Visibility = BarItemVisibility.Always;
                }

                else
                {
                    // Hide Menu
                    menu4Comment.Visibility = BarItemVisibility.Never;
                    menu5Export.Visibility = BarItemVisibility.Never;
                    menu6Reset.Visibility = BarItemVisibility.Never;
                }

                // Get HitInfo
                GridHitInfo hitInfo = this.m_grdViewActive.CalcHitInfo(e.Location);

                if (hitInfo.InRow)
                {
                    // Popup Menu
                    popupMenu.ShowPopup(Control.MousePosition);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdRequestView_DoubleClick(object sender, EventArgs e)
        {
            // Check
            if (grdRequestView.DataSource == null || grdRequestView.RowCount == 0) return;

            // Row
            var row = (TableRequest)grdRequestView.GetFocusedRow();

            // Row Handle
            int p_FocusedRowHandle = grdRequestView.FocusedRowHandle;

            // Closed Items
            if (row.PBN_Close_04 == alfaStr.OKEY) return;

            if (this.m_SessionInfo.Group == alfaStr.IT)
            {
                // Approved Items
                if (row.PBN_Apprv_02 == alfaStr.APPROVED)
                {
                    // Set Message
                    alfaCtrl.SetText(statusResult, "WARNING :  Not Allowed ... !", false); return;
                }
            }

            // Set Message
            alfaCtrl.SetText(statusResult, "OK", true);

            using (alfaDS DS = new alfaDS())
            {
                // Object
                var p_Request = DS.Context.TableRequest.First(tt => tt.ID == row.ID);

                // Create Form
                var frm = new FrmEdit(p_Request, this.m_SessionInfo);

                // Confirmation
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // SaveChanges
                    DS.Context.SaveChanges();

                    // List 
                    this.btnRequestList_Click(null, null);

                    // Focus
                    alfaGrid.SelectRow(grdRequestView, p_FocusedRowHandle);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdRequestView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                // Update 
                if (e.KeyCode == Keys.Enter && grdRequestView.RowCount > 0) this.grdRequestView_DoubleClick(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdRequestView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                // Get ID
                int p_ID = Convert.ToInt32(grdRequestView.GetRowCellValue(e.FocusedRowHandle, "ID"));

                // Set Log
                txtLog.Text = alfaEntity.GetRequestLog(p_ID);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu1SelectALL_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Select ALL
            this.SelectItems(true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu2UnSelectALL_ItemClick(object sender, ItemClickEventArgs e)
        {
            // UnSelect ALL
            this.SelectItems(false);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu4Ping_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (this.m_WAPI.IsRunning) return;

            // Call Ping
            if ( this.m_grdViewActive ==  grdRequestView) this.Ping_RequestIP(); else this.Ping_ListIP();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu5Export_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (this.m_grdViewActive != grdRequestView) return;

            // SaveDialog
            SaveFileDialog sf = new SaveFileDialog();

            // Set Properties
            sf.Filter = "XLS Files (*.xls)|*.xls";
 
            // Check for Cancel
            if (sf.ShowDialog() != DialogResult.OK) return;

            // Update UI
            this.Update();

            // Export to Target
            grdRequestView.ExportToXls(sf.FileName);

            // Result Message
            alfaMsg.Info("Exporting List is Successfully Done ... !");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Ping_RequestIP()
        {
            // Get List
            var listReq = (List<TableRequest>)grdRequestView.GridControl.DataSource;

            // Check Selected Items
            if ((from tt in listReq where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            // Get Checked Items
            var qry = from tt in listReq where tt.Check == true select tt;

            // Disable Controls
            alfaCtrl.DisableButtons(grpRequest);
            alfaCtrl.DisablePages(tabMain, pageRequest);

            // Set Control
            MainTimer.Tag = grpRequest;

            // Start Timer
            MainTimer.Start();

            // Send InfoBlox
            this.m_WAPI.PingIPv4List_SSH(qry.ToList(), this.m_SessionInfo, grdRequestView); 

            // Message
            alfaMsg.Info("Ping Process is Finished Successfully ...!");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Ping_ListIP()
        {
            // Get List
            var listIP = (List<rowIPv4>)grdIPv4View.GridControl.DataSource;

            // Check Selected Items
            if ((from tt in listIP where tt.Check == true select tt).Count() == 0)
            {
                // Show Warning
                alfaMsg.Error("WARNING :  Please Select at Least One Item ... !"); return;
            }

            // Get Checked Items
            var qry = from tt in listIP where tt.Check == true select tt;

            // Disable Controls
            alfaCtrl.DisableButtons(grpIPv4List);
            alfaCtrl.DisablePages(tabMain, pageIPv4);

            // Set Control
            MainTimer.Tag = grpIPv4List;

            // Start Timer
            MainTimer.Start();

            // Send InfoBlox
            this.m_WAPI.PingIPv4List_SSH(qry.ToList(), this.m_SessionInfo, grdIPv4View); 

            // Message
            alfaMsg.Info("Ping Process is Finished Successfully ... !");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu3Comment_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Hide <---> Show
            if (menu4Comment.Caption.Contains("Hide"))
            {
                splitContainerControl.SplitterPosition = 0;
                menu4Comment.Caption = menu4Comment.Caption.Replace("Hide", "Show");
            }
            else
            {
                splitContainerControl.SplitterPosition = 600;
                menu4Comment.Caption = menu4Comment.Caption.Replace("Show", "Hide");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menu6Reset_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Reset View
            grdRequestView.Columns.Clear();

            // Get List
            this.btnRequestList_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            // Check
            if (this.m_WAPI != null && this.m_WAPI.IsRunning)
            {
                alfaMsg.Error("Please Wait for Running WAPI Process ... !"); return;
            }

            // Restart
            Application.Restart();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Close
            if (!this.m_WAPI.IsRunning) this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusUser_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Check UserName
            if (string.IsNullOrEmpty(txtUser.Text)) return;

            // Set UserName
            this.m_SessionInfo.User = txtUser.Text;

            // Create Form
            FrmUser frm = new FrmUser(this.m_SessionInfo);

            // Call Form
            if (frm.ShowDialog() == DialogResult.OK)
            {
                statusUser.Caption = String.Format("{0}", this.m_SessionInfo.User);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnChartClear_Click(object sender, EventArgs e)
        {
            // Reset Totals
            txtTotalCancel.Text = "0";
            txtTotalDel.Text = "0";
            txtTotalDel.Text = "0";
            txtTotalEdit.Text = "0";
            txtTotalNew.Text = "0";
            txtTotalReject.Text = "0";

            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // DateTime01
            txtReportStart01.DateTime = dtNow.AddDays(-7);
            txtReportFinish01.DateTime = dtNow.AddDays(-0);

            // DateTime01
            txtReportStart02.DateTime = dtNow.AddDays(-14);
            txtReportFinish02.DateTime = dtNow.AddDays(-7);

            // Clear Chart
            chartReport.Series.Clear();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnChartList_Click(object sender, EventArgs e)
        {
            // Create Lists
            ArrayList p_Master = new ArrayList();
            ArrayList p_Referans = new ArrayList();

            // Master List
            p_Master.Add(new alfaSeries(1, "A", alfaEntity.Report_TotalValues_V1(txtReportStart01.DateTime, txtReportFinish01.DateTime, "IP_NEW")));
            p_Master.Add(new alfaSeries(2, "B", alfaEntity.Report_TotalValues_V1(txtReportStart01.DateTime, txtReportFinish01.DateTime, "IP_DEL")));
            p_Master.Add(new alfaSeries(3, "C", alfaEntity.Report_TotalValues_V1(txtReportStart01.DateTime, txtReportFinish01.DateTime, "IP_EDIT")));
            p_Master.Add(new alfaSeries(4, "D", alfaEntity.Report_TotalValues_V2(txtReportStart01.DateTime, txtReportFinish01.DateTime, "REJECTED")));
            p_Master.Add(new alfaSeries(5, "E", alfaEntity.Report_TotalValues_V2(txtReportStart01.DateTime, txtReportFinish01.DateTime, "CANCELED")));

            // Referans List
            p_Referans.Add(new alfaSeries(1, "A", alfaEntity.Report_TotalValues_V1(txtReportStart02.DateTime, txtReportFinish02.DateTime, "IP_NEW")));
            p_Referans.Add(new alfaSeries(2, "B", alfaEntity.Report_TotalValues_V1(txtReportStart02.DateTime, txtReportFinish02.DateTime, "IP_DEL")));
            p_Referans.Add(new alfaSeries(3, "C", alfaEntity.Report_TotalValues_V1(txtReportStart02.DateTime, txtReportFinish02.DateTime, "IP_EDIT")));
            p_Referans.Add(new alfaSeries(4, "D", alfaEntity.Report_TotalValues_V2(txtReportStart02.DateTime, txtReportFinish02.DateTime, "REJECTED")));
            p_Referans.Add(new alfaSeries(5, "E", alfaEntity.Report_TotalValues_V2(txtReportStart02.DateTime, txtReportFinish02.DateTime, "CANCELED")));

            // Assign Values
            txtTotalNew.Text = (p_Master[0] as alfaSeries).Value.ToString();
            txtTotalDel.Text = (p_Master[1] as alfaSeries).Value.ToString();
            txtTotalEdit.Text = (p_Master[2] as alfaSeries).Value.ToString();
            txtTotalReject.Text = (p_Master[3] as alfaSeries).Value.ToString();
            txtTotalCancel.Text = (p_Master[4] as alfaSeries).Value.ToString();

            // Create Series MM
            Series seriesMas = this.CreateSeries("MASTER", ViewType.Bar, p_Master, "Request", "Value", Color.Blue);
            Series seriesRef = this.CreateSeries("REFERANS", ViewType.Bar, p_Referans, "Request", "Value", Color.Red);
             
            // Add to Chart
            chartReport.Series.Clear();
            chartReport.Series.Add(seriesMas);
            chartReport.Series.Add(seriesRef);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private Series CreateSeries( string p_Name, ViewType p_ChartType, object p_DataSource, string p_Argument, string p_Scale, Color p_Color )
        {
            // Create Series
            Series seriesSatis = new Series(p_Name, p_ChartType);

            // Assign Datasource
            seriesSatis.DataSource = p_DataSource;

            // Set Series Color
            seriesSatis.View.Color = p_Color;

            // Argument ScaleType
            seriesSatis.ArgumentScaleType = ScaleType.Auto;
            seriesSatis.ArgumentDataMember = p_Argument;

            // Value ScaleType
            seriesSatis.ValueScaleType = ScaleType.Numerical;
            seriesSatis.ValueDataMembers.AddRange(new string[] { p_Scale });

            // Return
            return seriesSatis;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        
        private void GridView_Variant(bool p_Load)
        {
            // Check
            if (m_SessionInfo.User == null) return;

            try
            {
                // Get Path
                string p_Path = Application.StartupPath + "\\Variants";

                // Create Directory
                if (!Directory.Exists(p_Path)) Directory.CreateDirectory(p_Path);

                // Get FileNames
                string p_FileRequest = string.Format("{0}\\Variants\\grdRequestView_{1}.xml", Application.StartupPath, m_SessionInfo.User);

                if (p_Load)
                {
                    // ForceInitialize
                    grdRequest.ForceInitialize();

                    // Load Files
                    if (File.Exists(p_FileRequest)) grdRequestView.RestoreLayoutFromXml(p_FileRequest);
                }
                else
                {
                    // Save Files
                    grdRequestView.SaveLayoutToXml(p_FileRequest);
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Variants
            this.GridView_Variant(false);

            // Close Session
            alfaEntity.TableSession_Close(this.m_SessionInfo);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSessionClear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // Reset Values
            txtSession01.DateTime = dtNow.AddDays(-2);
            txtSession02.DateTime = dtNow.AddDays(+2);

            // Clear Grid
            grdSessionView.Columns.Clear();
            grdSessionView.GridControl.DataSource = null;

            // Focus
            grdSessionView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSessionList_Click(object sender, EventArgs e)
        {
            // Get List            
            var ListReq = alfaEntity.TableSession_GetList(txtSession01.DateTime, txtSession02.DateTime);
            
            // Set GridView
            grdSessionView.GridControl.DataSource = ListReq;
            alfaGrid.SetView(grdSessionView, true);
            grdSessionView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtRequestLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtRequestLevel.SelectedIndex == 0)
            {
                // Enable Control
                alfaCtrl.EnableControl(txtRequestDate01, Color.White);
                alfaCtrl.EnableControl(txtRequestDate02, Color.White);
            }
            else
            {
                // Disable Control
                alfaCtrl.DisableControl(txtRequestDate01, Color.Gray);
                alfaCtrl.DisableControl(txtRequestDate02, Color.Gray);
            }
        }

        private void txtNetwork_EditValueChanged(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//



    }
}