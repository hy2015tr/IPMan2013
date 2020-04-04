using System;
using System.Linq;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace IPMAN2013
{
    public partial class FrmSqlServer : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        private alfaSession m_Session = null;
        private string m_ConnectionString = null;

        //-------------------------------------------------------------------------------------------------------------//

        public FrmSqlServer(alfaSession p_Session)
        {
            // Initalize
            InitializeComponent();

            // Reset Message
            alfaCtrl.SetResult(txtResult, string.Empty, alfaResult.None);

            // Disable btnSave
            alfaCtrl.DisableControl(btnSave);

            // Save Client
            m_Session = p_Session;

            // Server Name
            Set_Database_Name();

            // Focus
            txtSQLServer.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void Set_Database_Name()
        {
            // Set Server
            txtSQLServer.Text = m_Session.DBName;
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmSqlServer_KeyDown(object sender, KeyEventArgs e)
        {
            // Set Buttons    
            alfaCtrl.EnableControl(btnTest);
            alfaCtrl.DisableControl(btnSave);
            
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();

            // Control + ENTER
            else if ((e.Control) && (e.KeyCode == Keys.Enter))
            {
                if (btnTest.Enabled)
                {
                    // btnSave
                    this.btnTest_Click(null, null);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmSqlServer_Shown(object sender, EventArgs e)
        {
            // Focus
            txtSQLServer.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Result
            string p_Result = null;

            // Test and Get Connection
            string p_ConnStr = alfaEntity.ConnStr_Test(txtSQLServer.Text, out p_Result);

            if (p_ConnStr != null)
            {
                // Reset Message
                alfaCtrl.SetResult(txtResult, p_Result, alfaResult.Pass);

                // Save
                this.m_ConnectionString = p_ConnStr;

                // Set Buttons
                alfaCtrl.EnableControl(btnSave);
                alfaCtrl.DisableControl(btnTest);
            }
            else
            {
                // Reset Message
                alfaCtrl.SetResult(txtResult, p_Result, alfaResult.Fail);

                // Save
                this.m_ConnectionString = null;

                // Set Buttons
                alfaCtrl.EnableControl(btnTest);
                alfaCtrl.DisableControl(btnSave);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save Settings
            alfaEntity.ConnStr_EnCrypt(this.m_ConnectionString);

            // Set Result
            this.DialogResult = DialogResult.OK;

            // Save Server
            this.m_Session.DBName = txtSQLServer.Text;

            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void txtSQLServer_KeyUp(object sender, KeyEventArgs e)
        {
            // Disable btnSave
            alfaCtrl.DisableControl(btnSave);

            if (txtSQLServer.Text.Length > 0)
            {
                // Enable btnTest
                alfaCtrl.EnableControl(btnTest);
            }
            else
            {
                // Disable btnTest
                alfaCtrl.DisableControl(btnTest);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}