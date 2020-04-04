using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net.Mail;
using System.Reflection;
using DevExpress.XtraTab;
using System.Diagnostics;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Columns;
using System.Security.Cryptography;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraVerticalGrid.Rows;

namespace IPMAN2013
{

    #region //-----------alfaItem-----------//

    public class alfaItem
    {
        // Fields
        private string m_EntityName;
        private string m_EntityText;

        // Properties
        public string Name { get { return this.m_EntityName;  }  }
        public string Text { get { return this.m_EntityText;  }  }

        // Constructor
        public alfaItem( string p_EntityText, string p_EntityName )
        {
            this.m_EntityName = p_EntityName;
            this.m_EntityText = p_EntityText;
        }

        // Override Method
        public override string ToString() { return this.m_EntityText; }
    }

    #endregion    


    #region //-----------alfaEnum-----------//

	public static class alfaResult
	{
		public static int None = 0;
		public static int Pass = 1;
        public static int Fail = 2;
	}

	# endregion


    #region //-----------alfaSec------------//

    public class alfaSec
    {
        //-------------------------------------------------------------------------------------------------//

        public static string Crypt(string s_Data, string s_Password, bool b_Encrypt) // Encryption & Decryption
        {
            byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };

            PasswordDeriveBytes i_Pass = new PasswordDeriveBytes(s_Password, u8_Salt);

            Rijndael i_Alg = Rijndael.Create();
            i_Alg.Key = i_Pass.GetBytes(32);
            i_Alg.IV = i_Pass.GetBytes(16);

            ICryptoTransform i_Trans = (b_Encrypt) ? i_Alg.CreateEncryptor() : i_Alg.CreateDecryptor();

            MemoryStream i_Mem = new MemoryStream();
            CryptoStream i_Crypt = new CryptoStream(i_Mem, i_Trans, CryptoStreamMode.Write);

            byte[] u8_Data;

            try
            {
                if (b_Encrypt) u8_Data = Encoding.Unicode.GetBytes(s_Data);
                else u8_Data = Convert.FromBase64String(s_Data);

                i_Crypt.Write(u8_Data, 0, u8_Data.Length);
                i_Crypt.Close();
            }
            catch { return null; }

            if (b_Encrypt) return Convert.ToBase64String(i_Mem.ToArray());
            else return Encoding.Unicode.GetString(i_Mem.ToArray());

        }

        //-------------------------------------------------------------------------------------------------//

        public static string EnCrypt(string  p_Message)
        {
            return alfaSec.Crypt(p_Message, "ALPHASOFT", true);
        }

        //-------------------------------------------------------------------------------------------------//

        public static string DeCrypt(string p_Message)
        {
            return alfaSec.Crypt(p_Message, "ALPHASOFT", false);
        }

        //-------------------------------------------------------------------------------------------------//
    }

    #endregion


    #region //-----------alfaMsg------------//

    public class alfaMsg
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void CursorWait()
        {
            // Wait Cursor
            Cursor.Current = Cursors.WaitCursor;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void CursorDefult()
        {
            // Wait Cursor
            Cursor.Current = Cursors.Default;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DialogResult Quest(string strMessage)
        {
            // Cursor
            Cursor.Current = Cursors.Default;

            // Show Message
            return MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DialogResult Error(string strMessage)
        {
            // Cursor
            Cursor.Current = Cursors.Default;

            // Show Message
            return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DialogResult Error(Exception p_Exception)
        {
            // Cursor
            Cursor.Current = Cursors.Default;

            // Message
            string strMessage = null;

            // Get Message
            if (p_Exception.InnerException != null)
            {
                if (p_Exception.InnerException.InnerException != null)
                {
                    strMessage = p_Exception.InnerException.InnerException.Message;
                }
                else strMessage = p_Exception.InnerException.Message;
            } 
            else strMessage = p_Exception.Message;

            // Show Message
            return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DialogResult Info(string strMessage)
        {
            // Cursor
            Cursor.Current = Cursors.Default;

            // Show Message
            return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion


    #region //-----------alfaStr------------//

	public class alfaStr
	{
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        #region //---Member Fields---//

        // Client Info
        public static string IT  = "IT";
        public static string ALL = "ALL";
        public static string PBN = "PBN";
        public static string OKEY = "OK"; 
        public static string USED = "USED";
        public static string ERROR = "ERROR"; 
        public static string RESET = "RESET";
        public static string IP_DEL = "IP_DEL"; 
        public static string IP_NEW = "IP_NEW"; 
        public static string DEFAULT = "---";
        public static string ACTIVE = "ACTIVE";
        public static string CLOSED = "CLOSED";
        public static string UNUSED = "UNUSED";
        public static string WAITING = "WAITING"; 
        public static string IP_EDIT = "IP_EDIT"; 
        public static string CABLING = "CABLING"; 
        public static string RESERVED = "RESERVED";
        public static string REJECTED = "REJECTED";
        public static string APPROVED = "APPROVED"; 
        public static string CANCELED = "CANCELED"; 
        public static string PROCESSING = "PROCESSING";
        public static string PING_TIMEOUT = "PING_TIMEOUT";
        public static string PING_REPLIED = "PING_REPLIED";

        #endregion

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string SetDefault(string p_Value)
        {
            if (p_Value.Trim().Length == 0) return alfaStr.DEFAULT; else return p_Value;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        
        public static void SetColumnsUpper( GridColumnCollection p_GridColumnCollection )
        {
            // Set UpperCase for Columns
            foreach( GridColumn col in p_GridColumnCollection ) 
            {                
                col.FieldName = col.FieldName.ToUpper( new CultureInfo("en-US") );
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetAppVersion(bool FullVersion)
        {
            // Get FileVersionInfo
            FileVersionInfo fi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            // Get App Version
            if (FullVersion) return "v" + fi.FileVersion;
            else return "v" + fi.FileMajorPart + "." + fi.FileMinorPart;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string ByteArrayToStringV1(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string ByteArrayToStringV2(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetEnglishText(string p_Text)
        {
            // Create Encodings
            Encoding iso = Encoding.GetEncoding("Cyrillic");
            Encoding utf8 = Encoding.UTF8;
            
            // Get Bytes
            byte[] utfBytes = utf8.GetBytes(p_Text);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                        
            // Return
            return iso.GetString(isoBytes);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion


    #region //-----------alfaLog------------//

    public class alfaLog
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Add( TableRequest p_Request, alfaSession p_Session, string p_Text)
        {
            // DateTime
            var dtNow = DateTime.Now;

            // Get Text
            var lstr = string.Format("{0} ({1}) {2} ", dtNow, p_Session.UserGroup(), p_Text);

            // Add Log
            p_Request.ReqLog += Environment.NewLine + lstr;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion


    #region //-----------alfaDate-----------//

    public class alfaDate
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetDate_V1(DateTime p_DateTime)  // YYYYMMDD
        {
            // Get Values
            string yrs = p_DateTime.Year.ToString("0000");
            string mon = p_DateTime.Month.ToString("00");
            string day = p_DateTime.Day.ToString("00");

            // Return
            return yrs + mon + day;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetDate_V2(DateTime p_DateTime) // YYYY-MM-DD
        {
            // Get Values
            string yrs = p_DateTime.Year.ToString("0000");
            string mon = p_DateTime.Month.ToString("00");
            string day = p_DateTime.Day.ToString("00");

            // Return
            return yrs + "-" + mon + "-" + day;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetDate_V3(DateTime p_DateTime) // YYYYMM
        { 
            // Get Values
            string yrs = p_DateTime.Year.ToString("0000");
            string mon = p_DateTime.ToString("MMMM", new CultureInfo("tr-TR", false)).ToUpper();

            // Return
            return yrs + " " + mon;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetTime(DateTime p_DateTime)
        {
            // Get Values
            string hrs = p_DateTime.Hour.ToString("00");
            string min = p_DateTime.Minute.ToString("00");
            string sec = p_DateTime.Second.ToString("00");

            // Return
            return hrs + min + sec;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int GetTotalSecs(DateTime p_DateTime)
        {
            // Get Values
            int sec_hrs = p_DateTime.Hour * 60 * 60;
            int sec_min = p_DateTime.Minute * 60;
            int sec_sec = p_DateTime.Second * 1;

            // Return
            return sec_hrs + sec_min + sec_sec;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int GetNumberOfDays(DateTime p_DateTime)
        {
            // Return
            return DateTime.DaysInMonth(p_DateTime.Year, p_DateTime.Month);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion


    #region //-----------alfaVer------------//

    public class alfaVer
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetAppVersion()
        {
            // Get Versions
            int verMajor = Assembly.GetExecutingAssembly().GetName().Version.Major;
            int verMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
            int verBuild = Assembly.GetExecutingAssembly().GetName().Version.Build;
            int verRevis = Assembly.GetExecutingAssembly().GetName().Version.Revision;

            // Return
            return "v" + verMajor + "." + verMinor + "." + verBuild  + "." + verRevis;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion

    
    #region //-----------alfaCtrl-----------//

    public class alfaCtrl
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void DisablePages( XtraTabControl p_TabControl, XtraTabPage p_Page)
        {
            foreach (XtraTabPage page in p_TabControl.TabPages)
            {
                if ( page != p_Page)
                {
                    page.PageEnabled = false;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void EnablePages(XtraTabControl p_TabControl)
        {
            foreach (XtraTabPage page in p_TabControl.TabPages)
            {
                page.PageEnabled = true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void DisableButtons(Control p_Control)
        {
            foreach (Control ctrl in p_Control.Controls)
            {
                if (ctrl.GetType().ToString() == "DevExpress.XtraEditors.SimpleButton")
                {
                    alfaCtrl.DisableControl((SimpleButton)ctrl);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void EnableButtons(Control p_Control)
        {
            foreach (Control ctrl in p_Control.Controls)
            {
                if (ctrl.GetType().ToString() == "DevExpress.XtraEditors.SimpleButton")
                {
                    alfaCtrl.EnableControl((SimpleButton)ctrl);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetButton(SimpleButton p_Button, bool p_Flag)
        {
            if (p_Flag) alfaCtrl.EnableControl(p_Button); else alfaCtrl.DisableControl(p_Button);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void EnableControl(SimpleButton p_Button)
        {
            if (p_Button.Tag == (object)"ActionButton")
            {
                p_Button.LookAndFeel.SkinName = "Glass Oceans";
                p_Button.ForeColor = Color.Black;
            }
            else
            {
                p_Button.LookAndFeel.SkinName = "Sharp";
                p_Button.ForeColor = Color.White;
            }
            p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
            p_Button.Enabled = true;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void DisableControl(SimpleButton p_Button)
        {
            p_Button.LookAndFeel.SkinName = "DevExpress Dark Style";
            p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
            p_Button.Enabled = false;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void EnableControl(Control p_Control, Color p_Color)
        {
            p_Control.Enabled = true;
            p_Control.BackColor = p_Color;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void DisableControl(Control p_Control, Color p_Color)
        {
            p_Control.Enabled = false;
            p_Control.BackColor = p_Color;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetText( BarStaticItem p_Control, string p_Message, bool p_Status )
        {
            // Set Message
            p_Control.Caption = p_Message;

            // Set Color
            if (p_Status) p_Control.ItemAppearance.Normal.ForeColor = Color.Lime;
                     else p_Control.ItemAppearance.Normal.ForeColor = Color.LightCoral;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    
		public static void SetResult( LabelControl p_txtResult, string p_ResultStr, int p_Status )
		{
			// Set Text
			p_txtResult.Text = p_ResultStr;

			// Set Colors
			switch ( p_Status )
			{
				case 0: p_txtResult.BackColor = Color.Gray; break;
				case 1: p_txtResult.BackColor = Color.Navy; break;
				case 2: p_txtResult.BackColor = Color.Red; break;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion

    
    #region //-----------alfaGrid-----------//

    public class alfaGrid
    {

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetView(GridView p_GridView, bool p_Status)
        {
            // Check GridView
            if (p_GridView.Columns.Count == 0) return;

            // Hide Columns
            alfaGrid.ColumnHide(p_GridView, "TableGroup-TableUser-ReqLog");

            // Set RowCount Position
            p_GridView.Columns[0].SummaryItem.DisplayFormat = "Count: {0}";
            p_GridView.Columns[0].SummaryItem.FieldName = p_GridView.Columns[0].FieldName;
            p_GridView.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;


            // DateTime Values
            foreach (var p_Field in "ReqStart-ReqStart-SessionStart-SessionFinish".Split('-'))
            {
                if (p_GridView.Columns[p_Field] != null)
                {
                    p_GridView.Columns[p_Field].DisplayFormat.FormatType = FormatType.DateTime;
                    p_GridView.Columns[p_Field].DisplayFormat.FormatString = "yyyy-MM-dd  HH:mm:ss";
                }
            }

            // Column ID
            if (p_GridView.Columns["ID"] != null)
            {
                p_GridView.Columns["ID"].OptionsColumn.AllowEdit = false;
            }

            // Password
            if (p_GridView.Columns["Password"] != null)
            {
                RepositoryItemTextEdit repItem = new RepositoryItemTextEdit();
                repItem.UseSystemPasswordChar = true;
                p_GridView.Columns["Password"].ColumnEdit = repItem;
            }

            // Check
            if (p_GridView.Columns["Check"] != null)
            {
                p_GridView.Columns["Check"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }

            // Show Footer
            p_GridView.OptionsView.ShowFooter = true;

            // Set View
            p_GridView.OptionsView.ColumnAutoWidth = false;
            p_GridView.BestFitColumns();

            // Make Editable
            p_GridView.OptionsBehavior.Editable = !p_Status;
            p_GridView.OptionsBehavior.ReadOnly = p_Status;

            // Add Lookups
            alfaGrid.AddLookup_Users(p_GridView, "ExtResponsible");
            alfaGrid.AddLookup_Priority(p_GridView, "ExtPriority");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ColumnReadOnly(GridView p_GridView, string p_List, bool p_Status)
		{ 
			foreach (var str in p_List.Split('-'))
			{
				// Get Row
                var row = p_GridView.Columns[str];

                if (row != null)
                {
				    // Make Readonly
                    row.OptionsColumn.AllowEdit = !p_Status;
                    row.OptionsColumn.ReadOnly = p_Status;
                }
			}
		}
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SelectRow(GridView p_GridView, int p_RowHandle)
        {
            // Unselect Current Row
            p_GridView.UnselectRow(p_GridView.FocusedRowHandle);

            // Set New Position
            p_GridView.SelectRow(p_RowHandle);
            p_GridView.FocusedRowHandle = p_RowHandle;

            // Focus
            p_GridView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ColumnHide(GridView p_GridView, string p_Columns)
        {
            foreach (string strColName in p_Columns.Split('-'))
            {
                if (p_GridView.Columns[strColName] != null)
                {
                    // Hide Columns
                    p_GridView.Columns.ColumnByFieldName(strColName).Visible = false;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void AddLookup_Users(GridView p_Grid, string p_Value)
		{
            // Get Column
            var Col = p_Grid.Columns[p_Value];

            // Check
            if (Col == null) return;

			// Create Lookup
            RepositoryItemComboBox p_LookUpCombo = new RepositoryItemComboBox();

            // Get Data List
            using (alfaDS DS = new alfaDS())
            {
                // Get List
                var listUser = DS.Context.TableUser.Where(tt => tt.TableGroup.Name == "IT").Select(tt => tt);

                // Add Item
                foreach (var user in listUser) p_LookUpCombo.Items.Add(user.FullName);
            }

            // Assign to Grid
            Col.ColumnEdit = p_LookUpCombo;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void AddLookup_Priority(GridView p_Grid, string p_Value)
		{
            // Get Column
            var Col = p_Grid.Columns[p_Value];

            // Check
            if (Col == null) return;

			// Create Lookup
            RepositoryItemComboBox p_LookUpCombo = new RepositoryItemComboBox();

            // Priorty Levels
            p_LookUpCombo.Items.Add("(1) LOW");
            p_LookUpCombo.Items.Add("(2) MEDIUM");
            p_LookUpCombo.Items.Add("(3) VERY HIGH");
            
            // Assign to Grid
            Col.ColumnEdit = p_LookUpCombo;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion


	#region //-----------alfaVGrid----------//

	public class alfaVGrid 
	{ 
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetPropertyGrid(PropertyGridControl p_Grid, Object p_Object)
        {
            // Set Object
            p_Grid.Rows.Clear();
            p_Grid.RecordWidth = 140;
            p_Grid.SelectedObject = null;
            p_Grid.SelectedObject = p_Object;
            
            // Row Hide
            alfaVGrid.RowHide(p_Grid, "TableGroup-TableUser-TaskID-PingStatus");

            // Row Readonly
            alfaVGrid.RowReadOnly(p_Grid, "ID-TaskID");

            // Add LookUp
            alfaVGrid.AddLookUp(p_Grid, "GroupID", "TableGroup", "ID", "Name");

            // Add TextEdits
            alfaVGrid.AddTextEdits(p_Grid);

            // Add Lookups
            alfaVGrid.AddLookup_Users(p_Grid, "ExtResponsible");
            alfaVGrid.AddLookup_Priority(p_Grid, "ExtPriority");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void AddLookup_Users(PropertyGridControl p_Grid, string p_Value)
		{
		    // Get Row
            var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Value);

            // Check
            if (row == null) return;

			// Create Lookup
            RepositoryItemComboBox p_LookUpCombo = new RepositoryItemComboBox();

            // Get Data List
            using (alfaDS DS = new alfaDS())
            {
                // Get List
                var listUser = DS.Context.TableUser.Where(tt => tt.TableGroup.Name == "IT").Select(tt => tt);

                // Add Item
                foreach (var user in listUser) p_LookUpCombo.Items.Add(user.FullName);
            }

            // Assign to Grid
            row.Properties.RowEdit = p_LookUpCombo;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void AddLookup_Priority(PropertyGridControl p_Grid, string p_Value)
		{
		    // Get Row
            var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Value);

            // Check
            if (row == null) return;

			// Create Lookup
            RepositoryItemComboBox p_LookUpCombo = new RepositoryItemComboBox();

            // Priorty Levels
            p_LookUpCombo.Items.Add("(1) LOW");
            p_LookUpCombo.Items.Add("(2) MEDIUM");
            p_LookUpCombo.Items.Add("(3) VERY HIGH");
            
            // Assign to Grid
            row.Properties.RowEdit = p_LookUpCombo;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddTextEdits(PropertyGridControl p_Grid)
        {
            foreach (BaseRow row in p_Grid.Rows[0].ChildRows)
            {
                // Check
                if ("GroupID-Active-Admin-ExtVM-ExtBackup-ExtPortSecurity".Contains(row.Properties.Caption)) continue;

                // Add TextEdit
                alfaVGrid.AddTextEdit(p_Grid, row.Properties.Caption);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void RowReadOnly(PropertyGridControl p_Grid, string p_List)
		{ 
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			foreach (var str in p_List.Split('-'))
			{
				// Get Row
				var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(str);

				// Make Readonly
				if (row != null) row.Properties.ReadOnly = true;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void RowHide(PropertyGridControl p_Grid, string p_List)
		{
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			foreach (var str in p_List.Split('-'))
			{
				// Get Row
				var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(str);

				// Make Readonly
				if (row != null) row.Visible = false;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddTextEdit(PropertyGridControl p_Grid, string p_Field)
        {
            // Check Count
            if (p_Grid.Rows.Count == 0) return;

            // Create Lookup
            RepositoryItemTextEdit repTextEdit = new RepositoryItemTextEdit();

            // Get Row
            var Row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Field);

            if (Row != null)
            {
                // Password Char
                if (p_Field == "Password") repTextEdit.UseSystemPasswordChar = true;

                // Assign to Grid
                Row.Properties.RowEdit = repTextEdit;
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void AddLookUp(PropertyGridControl p_Grid, string p_Field, string p_DataSource, string p_ValueMember, string p_DisplayMember)
		{
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			// Create Lookup
			RepositoryItemLookUpEdit LookUp = new RepositoryItemLookUpEdit();

			// UpperCase
            LookUp.CharacterCasing = CharacterCasing.Upper;
            
            // Clear NullText
			LookUp.NullText = string.Empty;

			// Set LookUp Properties
            LookUp.ValueMember = p_ValueMember;
			LookUp.DisplayMember = p_DisplayMember;
            LookUp.SearchMode = SearchMode.AutoComplete;
			LookUp.TextEditStyle = TextEditStyles.Standard;
			LookUp.DataSource = alfaEntity.Entity_Get(p_DataSource);

            // Get Row
            var Row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Field);

            // Add Lookup
            if (Row != null) Row.Properties.RowEdit = LookUp;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion    


    #region //-----------alfaSession--------//

    public class alfaSession
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public string PC;
		public string DBName;
        public string LocIP;
        public string Active;
        public string OsVer;
        public string AppVer;
        public string NetVer;
        public string User;
        public string Group;
        public string Email;
        public string Date;
        public string Time;
        public string ReqStatus;
        public int SessionID;
        public bool Admin;

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public alfaSession()
        {
            // PC
            this.PC = System.Net.Dns.GetHostName();

		    // SQL Server
			using (alfaDS ent = new alfaDS())
			{
				this.DBName = ent.Context.Database.Connection.DataSource;
			}

            // IP Adress List
            System.Net.IPAddress[] ListIP = System.Net.Dns.GetHostEntry(this.PC).AddressList;

            // Local IP
            foreach (System.Net.IPAddress ip in ListIP)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    this.LocIP = ip.ToString();
                }
            }

            // Operating System Version
            this.OsVer = System.Environment.OSVersion.ToString();

            // Status
            this.Active = "Y";

            // Application Version
            this.AppVer = alfaVer.GetAppVersion();

            // Net Framework Version
            this.NetVer = System.Environment.Version.ToString();

            // UserName
            this.User = null;

            // GetDT
            DateTime dtNow = DateTime.Now;

            // Date
            this.Date = alfaDate.GetDate_V1(dtNow);

            // Time
            this.Time = alfaDate.GetTime(dtNow);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public string UserGroup()
        {
            // User + Group
            string p_String =  String.Format("{0} @ {1}", this.User, this.Group);

            // Return with Admin
            if (this.Admin) return p_String + " (Admin)"; else return p_String;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void RefreshLoginDateTime()
        {
            // GetDT
            DateTime dtNow = DateTime.Now;

            // Get Date
            this.Date = alfaDate.GetDate_V1(dtNow);

            // Get Time
            this.Time = alfaDate.GetTime(dtNow);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion
    

    #region //-----------alfaMail-----------//

    public class alfaMail
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string CreateBody(string p_TaskID, string p_ListIP, alfaSession p_Session, string p_ReqType, string p_Comment)
        {
            // Body Text
            string p_Body = "==================================================================\n";

            // TaskID
            p_Body += string.Format("TaskID \t: {0}\n", p_TaskID);

            // Submitter
            p_Body += string.Format("Submitter \t: {0} ({1}) \n", p_Session.User, p_Session.Group);

            // Request Type
            p_Body += string.Format("ReqType \t: {0} ({1})\n", p_ReqType, p_Session.ReqStatus);

            // Submit Time
            p_Body += string.Format("DateTime \t: {0} \n", DateTime.Now);

            // Affected Objects
            p_Body += string.Format("Objects \t: {0} \n", p_ListIP);

            // Comments
            p_Body += string.Format("Comment \t: {0} \n", p_Comment);

            // Version
            p_Body += string.Format("Version \t: {0} \n",p_Session.AppVer);

            // Line
            p_Body += "==================================================================\n";

            //Return
            return p_Body;
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//
         
        public static void Send(String p_To, String p_Cc, String p_Subject, String p_Body)
        {
            try
            {
                // Smtp
                SmtpClient p_Smtp = new SmtpClient();
                
                // Message
                MailMessage p_Message = new MailMessage();
                
                // Set Properties
                p_Message.From = new MailAddress("PBN - IP Manager<IPMAN2013@avea.com.tr>");
                p_Message.Subject = p_Subject;

                // Clear
                p_Message.To.Clear();
                p_Message.CC.Clear();
                
                // Add TO
                foreach (string p_mail in p_To.Split(';')) p_Message.To.Add(p_mail);

                // Add CC
                if (p_Cc != null) p_Message.CC.Add(p_Cc);
                
                // Body
                p_Message.Body = p_Body;
                p_Message.IsBodyHtml = false;
                
                // Set Properties
                p_Smtp.Host = "intmailrelay.tt-tim.tr";
                p_Smtp.Port = 25;
                p_Smtp.Send(p_Message);
                p_Smtp = null;
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion


    #region //-----------alfaSeries---------//

    public class alfaSeries
    {
        //---------------------------------------------------------------------------//

        public int ID { get; set; }
        public string Request { get; set; }
        public int Value { get; set; }

        //---------------------------------------------------------------------------//

        public alfaSeries( int p_ID, string p_Request, int p_Value )
        {
            // Constructor
            this.ID = p_ID;
            this.Request = p_Request;
            this.Value = p_Value;
        }

        //---------------------------------------------------------------------------//
    }

    #endregion

}
 