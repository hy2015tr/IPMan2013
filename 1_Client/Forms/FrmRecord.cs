using System;
using System.IO.Ports;
//using System.Data.Objects;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace IPMAN2013
{
    public partial class FrmRecord : DevExpress.XtraEditors.XtraForm
    {

        //-------------------------------------------------------------------------------------------------------------//

        public FrmRecord(string p_Title, Object p_Object)
        {
            // Initalize
            InitializeComponent();

            // Set TableName
            txtTabloAdi.Text = p_Title;

            // Set Object
            alfaVGrid.SetPropertyGrid(propGrid, p_Object);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmRecord_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Result
            this.DialogResult = DialogResult.OK;
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmRecord_Shown(object sender, EventArgs e)
        {
            // Focus
            propGrid.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void propGrid_KeyUp(object sender, KeyEventArgs e)
        {
            // Control + ENTER
            if (e.Control==true && e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(null, null);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}