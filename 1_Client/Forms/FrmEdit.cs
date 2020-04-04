using System;
using System.IO.Ports;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;


namespace IPMAN2013
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {

        //-------------------------------------------------------------------------------------------------------------//

        alfaSession m_Session = null;
        TableRequest m_Reqeuest = null;

        //-------------------------------------------------------------------------------------------------------------//

        public FrmEdit(TableRequest p_Reqeuest, alfaSession p_Session)
        {
            // Initalize
            InitializeComponent();

            // Set Session
            this.m_Session = p_Session;

            // Set Request
            this.m_Reqeuest = p_Reqeuest;

            // Set Title
            txtTitle.Text = p_Reqeuest.IPAddress;

            // Set Log
            txtLog.Text = p_Reqeuest.ReqLog;

            // Select Page
            tabControl.SelectedTabPage = pageRow;

            // Disable BtnSave
            // alfaCtrl.DisableControl(btnSave);

            // Set Object
            alfaVGrid.SetPropertyGrid(propGrid, p_Reqeuest);

            // Hide Columns
            alfaVGrid.RowHide(propGrid, "ID-Check-ReqStart-ReqFinish-ReqUser-ReqLog-ReqType-USR_Request_01-PBN_Apprv_02-CBL_Switch_03-PBN_Close_04-Ping-IPStatus-IPAddress-Network-IsConflict-MacAddress");

            if (p_Session.Group == alfaStr.IT)
            {
                // ReadOnly Columns
                alfaVGrid.RowReadOnly(propGrid, "ExtModulePort1-ExtModulePort2-ExtSwitch1-ExtSwitch2-ExtVlanNo");
            }
            if (p_Session.Group == alfaStr.CABLING)
            {
                // ReadOnly Columns I
                alfaVGrid.RowReadOnly(propGrid, "ExtPriority-ExtSite-ExtNote-ExtServerName-ExtResponsible-ExVM-ExBackup-ExtPortSecurity-ExtVlanNo");

                // ReadOnly Columns II
                if((bool)p_Reqeuest.ExtVM==true) alfaVGrid.RowReadOnly(propGrid, "ExtModulePort1-ExtModulePort2-ExtSwitch1-ExtSwitch2");

                // ReadOnly Columns III
                else if((bool)p_Reqeuest.ExtBackup==false) alfaVGrid.RowReadOnly(propGrid, "ExtModulePort2-ExtSwitch2");
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Result
            this.DialogResult = DialogResult.OK;

            // Get Comment
            string p_Comment = txtComment.Text;

            // Format Comment
            if (p_Comment != string.Empty) p_Comment = string.Format(" ({0})", p_Comment);

            // Add Log
            alfaLog.Add(this.m_Reqeuest, this.m_Session, "Request was Edited" + p_Comment);

            // Check Levels
            if (this.m_Session.Group == alfaStr.CABLING && this.m_Reqeuest.PBN_Apprv_02 == alfaStr.APPROVED)
            {
                // Cable Switch OK
                this.m_Reqeuest.CBL_Switch_03 = alfaStr.OKEY;
            }

            else if (this.m_Session.Group == alfaStr.CABLING && this.m_Reqeuest.PBN_Apprv_02 == alfaStr.REJECTED)
            {
                // Cable Switch OK
                this.m_Reqeuest.CBL_Switch_03 = alfaStr.OKEY;

                // PBN Waiting
                this.m_Reqeuest.PBN_Apprv_02 = alfaStr.WAITING;
            }

            else if (this.m_Session.Group == alfaStr.IT && this.m_Reqeuest.PBN_Apprv_02 == alfaStr.REJECTED)
            {
                // PBN Waiting
                this.m_Reqeuest.PBN_Apprv_02 = alfaStr.WAITING;
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmEdit_Shown(object sender, EventArgs e)
        {
            // Focus
            txtComment.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmEdit_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void propGrid_KeyUp(object sender, KeyEventArgs e)
        {
            // Control + ENTER
            if (e.Control == true && e.KeyCode == Keys.Enter && btnSave.Enabled == true)
            {
                this.btnSave_Click(null, null);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}