using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;


namespace IPMAN2013
{
    public partial class FrmUser : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        private alfaSession m_Client = null;

        //-------------------------------------------------------------------------------------------------------------//

        public FrmUser(alfaSession p_Client)
        {
            // Initalize
            InitializeComponent();

            // Save Client
            m_Client = p_Client;

            // Set UserName
            txtUserName.Text = m_Client.User;
            
            // Disable UserName
            alfaCtrl.DisableControl(txtUserName, Color.LightGray);

            // Set Button
            alfaCtrl.SetButton(btnChange, txtPassNew01.Text.Length > 0 && txtPassOld.Text.Length > 0);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmUser_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();

            // Control + ENTER
            else if ((e.Control) && (e.KeyCode == Keys.Enter))
            {
                if (btnChange.Enabled)
                {
                    // btnSave
                    this.btnChange_Click(null, null);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmArac_Shown(object sender, EventArgs e)
        {
            // Focus
            txtPassOld.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Disable btnChange
            alfaCtrl.DisableControl(btnChange);
            
            // Clear Passwords
            txtPassOld.Text = string.Empty;
            txtPassNew01.Text = string.Empty;

            // Focus
            txtPassOld.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (alfaEntity.UpdateUserPassword(txtUserName.Text, txtPassOld.Text, txtPassNew01.Text))
            {
                // Result
                this.DialogResult = DialogResult.OK;

                // Close
                this.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void txtALL_KeyUp(object sender, KeyEventArgs e)
        {
            // Set Button
            alfaCtrl.SetButton(btnChange, txtPassNew01.Text.Length > 0 && txtPassNew02.Text.Length > 0 && txtPassOld.Text.Length > 0 && txtPassNew01.Text == txtPassNew02.Text);
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}