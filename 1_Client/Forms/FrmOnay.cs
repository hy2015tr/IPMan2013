using System;
using System.Linq;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace IPMAN2013
{
    public partial class FrmOnay : DevExpress.XtraEditors.XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private alfaSession m_Session = null;
        private List<TableRequest> m_ListRequest = null;

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmOnay(alfaSession p_Session, List<TableRequest> p_ListRequest)
        {
            // Initalize
            InitializeComponent();

            // Session
            this.m_Session = p_Session;

            // List Request
            this.m_ListRequest = p_ListRequest;

            // Enable btnSave
            alfaCtrl.DisableControl(btnSave);

            // Focus
            lbTitle.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Cursor
            alfaMsg.CursorWait();

            // Get Status
            string p_Status = radioRequest.Properties.Items[radioRequest.SelectedIndex].Value.ToString();

            // Set Request Status
            this.RequestStatus(p_Status);

            // Set Request Status
            this.m_Session.ReqStatus = p_Status;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void RequestStatus(string p_Status)
        {
            // Get Comment
            string p_Comment = txtComment.Text;

            // Format Comment
            if (p_Comment != string.Empty) p_Comment = string.Format(" ({0})", p_Comment);

            foreach (var row in this.m_ListRequest)
            {
                if (row.Check)
                {
                    // Update Request Status
                    alfaEntity.TableRequest_Approval(row.ID, p_Status, this.m_Session, p_Comment);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void radioRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable btnSave
            alfaCtrl.EnableControl(btnSave);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//


    }
}