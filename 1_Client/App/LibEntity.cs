using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Data.Entity.Core.EntityClient;


namespace IPMAN2013
{

    #region //----------- AlfaDS -----------//


    //===============================================================================//

    public partial class IPMAN2013Entities // ---> ENTITY PARTIAL CLASS <---- 
    {
        public IPMAN2013Entities(string p_ConnStr) : base(p_ConnStr)
        {
            // TODO
        }
    }

    //===============================================================================//

    public class alfaDS : IDisposable
    {

        // ConnStr
        public static string m_ConnStr = null;

        // Context
        public IPMAN2013Entities Context = null;

        //--------------------------------------------------------------------//

        public alfaDS()
        {
            if (alfaDS.m_ConnStr == null)
            {
                // Get Connection
                alfaDS.m_ConnStr = alfaEntity.ConnStr_DeCrypt();

                if (alfaDS.m_ConnStr == null)
                {
                    // Message
                    alfaMsg.Error("ERROR = SQL ConnectionString is Not Valid ...!");

                    // Close Application
                    System.Environment.Exit(1);
                }
            }

            // Create Entity Context
            this.Context = new IPMAN2013Entities(alfaDS.m_ConnStr);

            // Disable LazyLoading
            this.Context.Configuration.LazyLoadingEnabled = false;
        }

        //--------------------------------------------------------------------//

        public alfaDS(string p_ConnStr)
        {
            // Create Entity Context
            this.Context =  new IPMAN2013Entities(p_ConnStr);

            // Disable LazyLoading
            this.Context.Configuration.LazyLoadingEnabled = false;
        }

        //--------------------------------------------------------------------//

        public void Dispose()
        {
            // Dispose
            this.Context.Dispose();
        }

        //--------------------------------------------------------------------//
    }

    # endregion 

    
    #region //-----------DataModel----------//


    public class rowSubnet
    {
        public bool Check { get; set; }
        public string Network { get; set; }
        public string Comment { get; set; }
        public string ExtSite { get; set; }
        public string ExtNote { get; set; }
        public string ExtServerName { get; set; }
        public string ExtResponsible { get; set; }
        public string ExtModulePort1 { get; set; }
        public string ExtModulePort2 { get; set; }
        public string ExtSwitch1 { get; set; }
        public string ExtSwitch2 { get; set; }
        public string ExtVlanNo { get; set; }
    }


    public class rowIPv4
    {
        public bool Check { get; set; }
        public string IPAddress { get; set; }
        public string Network { get; set; }
        public string ReqStatus { get; set; }
        public string PingStatus { get; set; }
        public string IPStatus { get; set; }
        public string IsConflict { get; set; }
        public string MacAddress { get; set; }
        public string ExtPriority { get; set; }
        public string ExtSite { get; set; }
        public string ExtNote { get; set; }
        public string ExtServerName { get; set; } 
        public string ExtResponsible { get; set; }
        public bool   ExtVM { get; set; }
        public bool   ExtBackup { get; set; }
        public bool   ExtPortSecurity { get; set; }
        public string ExtModulePort1 { get; set; }
        public string ExtModulePort2 { get; set; }
        public string ExtSwitch1 { get; set; }
        public string ExtSwitch2 { get; set; }
        public string ExtVlanNo { get; set; }
    }


    public partial class TableRequest
    {
        public bool Check { get; set; }
    }

    #endregion  


    #region //-----------alfaEntity---------//

    class alfaEntity
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string EntityObjectName = "IPMAN2013Entities";

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int GetNextTaskID()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                int p_SeqTaskID = DS.Context.Database.SqlQuery<int>("SELECT NEXT VALUE FOR SeqTaskID").First();

                // Return
                return p_SeqTaskID;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool UpdateUserPassword(string p_User, string p_PassOld, string p_PassNew)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableUser
                              where tt.UserName == p_User
                                 && tt.Password == p_PassOld
                              select tt;

                    List<TableUser> tbUser = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Check User
                    if (tbUser.Count == 0)
                    {
                        alfaMsg.Error("Old Password is Incorrect ...!"); return false;
                    }

                    // Set New Password
                    tbUser[0].Password = p_PassNew;

                    // Save
                    ent.Context.SaveChanges();

                    // Sucess
                    alfaMsg.Info("Yave have Successfully Changed Your Password ...!");  return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool CheckUserLogin(string p_User, string p_Pass, ref alfaSession p_Session)
        {
            using (alfaDS DS = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Reset
                //p_Group = null;

                // Query
                var qry = from tt in DS.Context.TableUser
                          where tt.UserName == p_User
                             && tt.Active == true
                          select new { tt.UserName, tt.Password, tt.Admin, tt.Email, GroupName=tt.TableGroup.Name };

                var tbUser = qry.ToList();

                // CursorDefult
                alfaMsg.CursorDefult();

                // Check User
                if (tbUser.Count == 0)
                {
                    alfaMsg.Error("User Name Could not be Found  ...!"); return false;
                }

                // Check Pass
                else if (tbUser[0].Password != p_Pass)
                {
                    alfaMsg.Error("You Entered Wrong Password ...!"); return false;
                }

                // Set GroupName
                p_Session.Group = tbUser[0].GroupName;
                p_Session.Admin = (bool)tbUser[0].Admin;
                p_Session.Email = tbUser[0].Email;

                // Return
                return true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetUserEmail(string p_UserName)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableUser
                          where tt.UserName == p_UserName
                          select tt;

                if (qry.Count() != 1) return null; else return qry.First().Email;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic TableUser_GetList(int? p_ID, bool? p_Active)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableUser
                          select new
                          {
                              tt.ID,
                              tt.UserName,
                              tt.Password,
                              tt.FullName,
                              tt.Email,
                              Group = tt.TableGroup.Name,
                              tt.Admin,
                              tt.Active,
                          };

                // Optional Prm1
                if (p_ID != null) qry = qry.Where(tt => tt.ID == p_ID);

                // Optional Prm2
                if (p_Active != null) qry = qry.Where(tt => tt.Active == p_Active);

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableGroup TableGroup_Get(string p_Name)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableGroup
                          where tt.Name == p_Name
                          select tt;

                // Return
                return qry.First();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableGroup> TableGroup_GetList(int? p_ID, bool? p_Active)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableGroup select tt;

                // Optional Prm1
                if (p_ID != null) qry = qry.Where(tt => tt.ID == p_ID);

                // Optional Prm2
                if (p_Active != null) qry = qry.Where(tt => tt.Active == p_Active);

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool TableRequest_CheckStatus(string p_IPAddress)
        { 
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableRequest
                          where tt.IPAddress == p_IPAddress
                             && !"OK-CANCELED".Contains(tt.PBN_Close_04)
                          select tt;

                // Get List
                var list = qry.ToList();

                // Return
                if (list.Count > 0 ) return true; else return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetRequestLog(int p_ID)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableRequest where tt.ID == p_ID select tt;

                // Fail
                if (qry.Count() == 0) return null;

                // Return
                else return qry.First().ReqLog;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableRequest> TableRequest_GetList(DateTime p_DateStart, DateTime p_DateFinish, int p_Level, string p_ReqType)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableRequest where tt.ReqStart >= p_DateStart && tt.ReqStart <= p_DateFinish select tt;

                // Level Filter
                     if (p_Level == 1) qry = DS.Context.TableRequest.Where(tt => tt.USR_Request_01 == alfaStr.OKEY && "WAITING-REJECTED".Contains(tt.PBN_Apprv_02) && tt.CBL_Switch_03 == alfaStr.WAITING && tt.PBN_Close_04 == alfaStr.WAITING);
                else if (p_Level == 2) qry = DS.Context.TableRequest.Where(tt => tt.USR_Request_01 == alfaStr.OKEY && tt.PBN_Apprv_02 == alfaStr.APPROVED && tt.CBL_Switch_03 == alfaStr.WAITING && tt.PBN_Close_04 == alfaStr.WAITING);
                else if (p_Level == 3) qry = DS.Context.TableRequest.Where(tt => tt.USR_Request_01 == alfaStr.OKEY && tt.PBN_Apprv_02 == alfaStr.APPROVED && tt.CBL_Switch_03 == alfaStr.OKEY && "WAITING-ERROR".Contains(tt.PBN_Close_04));

                // Optional Parameter
                if (p_ReqType != "IP_ALL") qry = qry.Where(tt => tt.ReqType == p_ReqType);

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool TableRequest_Add(rowIPv4 p_RowIP, alfaSession p_Session, int p_TaskID, string p_ReqType)
        {
            using (alfaDS DS = new alfaDS())
            {
                TableRequest p_Req = new TableRequest();

                // Copy
                alfaEntity.Copy(p_RowIP, p_Req);

                // DateTime Fields
                p_Req.ReqFinish = null;
                p_Req.ReqStart = DateTime.Now;

                // Check Double Request
                var qry = DS.Context.TableRequest.Where(tt => tt.ReqFinish == null && tt.IPAddress == p_Req.IPAddress).ToList();

                if (qry.Count > 0)
                {
                    // Error
                    alfaMsg.Error(p_Req.IPAddress + " was Already Reserved ... !"); return false;
                }

                // Req Type
                p_Req.ReqType = p_ReqType;

                // Add Log
                alfaLog.Add(p_Req, p_Session, "Request " + p_Req.ReqType + " was Created");

                // Set Request Levels
                p_Req.USR_Request_01 = alfaStr.OKEY;
                p_Req.PBN_Apprv_02 = alfaStr.WAITING;
                p_Req.CBL_Switch_03 = alfaStr.WAITING;
                p_Req.PBN_Close_04 = alfaStr.WAITING;

                // Set User
                p_Req.ReqUser = p_Session.User;

                // Set TaskID
                p_Req.TaskID = p_TaskID;

                // Add
                DS.Context.TableRequest.Add(p_Req);

                // Save
                DS.Context.SaveChanges();

                // Return
                return true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableRequest_Update(int p_TaskID, alfaSession p_Session)
        {
            using (alfaDS DS = new alfaDS())
            {
                foreach (var p_Req in DS.Context.TableRequest.Where(tt => tt.TaskID == p_TaskID).ToList())
                {
                    // DateTime Fields
                    p_Req.ReqFinish = null;
                    p_Req.ReqStart = DateTime.Now;

                    // Set Request Levels
                    p_Req.USR_Request_01 = alfaStr.OKEY;
                    p_Req.PBN_Apprv_02 = alfaStr.APPROVED;
                    p_Req.CBL_Switch_03 = alfaStr.OKEY;
                    p_Req.PBN_Close_04 = alfaStr.WAITING;

                    // Set User
                    p_Req.ReqUser = p_Session.User;
                }

                // Save
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableRequest_Update(TableRequest p_ReqUpdate)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableRequest where tt.ID == p_ReqUpdate.ID select tt;

                // Request
                var p_ReqDB = qry.First();

                // Update
                alfaEntity.Copy(p_ReqUpdate, p_ReqDB);

                // Save
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableRequest_Approval(int p_ID, string p_Status, alfaSession p_Session, string p_Comment)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableRequest where tt.ID == p_ID select tt;

                // Get Row
                var row = qry.SingleOrDefault();

                // Closed Items
                if ("OK-CANCELED".Contains(row.PBN_Close_04)) return;

                // Check
                if (row == null) return;

                if (p_Status == alfaStr.CANCELED)
                {
                    // Close Item
                    row.USR_Request_01 = alfaStr.CANCELED;
                    row.PBN_Apprv_02 = alfaStr.CANCELED;
                    row.CBL_Switch_03 = alfaStr.CANCELED;
                    row.PBN_Close_04 = alfaStr.CANCELED;
                    row.ReqFinish = DateTime.Now;
                }

                else
                {
                    // PBN Approval
                    row.PBN_Apprv_02 = p_Status;

                    if (row.PBN_Apprv_02 != alfaStr.APPROVED)
                    {
                        // Reset Switch
                        row.CBL_Switch_03 = alfaStr.WAITING;
                    }

                    else if (row.PBN_Apprv_02 == alfaStr.APPROVED && "IP_EDIT-IP_DEL".Contains(row.ReqType))
                    {
                        // Reset Switch
                        row.CBL_Switch_03 = alfaStr.OKEY;
                    }

                    else if (row.PBN_Apprv_02 == alfaStr.APPROVED && row.ExtVM == true)
                    {
                        // Reset Switch
                        row.CBL_Switch_03 = alfaStr.OKEY;
                    }
                }
   
                // Add Log
                alfaLog.Add(row, p_Session, "Request --> " + row.PBN_Apprv_02 + p_Comment);

                // Save
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableSubnet> TableSubnet_GetList(int? p_ID, bool? p_Active)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableSubnet select tt;

                // Optional Prm1
                if (p_ID != null) qry = qry.Where(tt => tt.ID == p_ID);

                // Optional Prm2
                if (p_Active != null) qry = qry.Where(tt => tt.Active == p_Active);

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableSubnet_ComboBox(ComboBoxEdit p_ComboBox)
        {
            // Get List
            List<TableSubnet> listTemp = alfaEntity.TableSubnet_GetList(null, true);

            // Clear List
            p_ComboBox.Properties.Items.Clear();

            foreach (var row in listTemp)
            {
                // Add Item
                p_ComboBox.Properties.Items.Add(row.Name);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableServer> TableServer_GetList(int? p_ID, bool? p_Active)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableServer select tt;

                // Optional Prm1
                if (p_ID != null) qry.Where(tt => tt.ID == p_ID);

                // Optional Prm2
                if (p_Active != null) qry.Where(tt => tt.Active == p_Active);

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableServer> TableServer_GetList(string p_Name)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableServer 
                          where tt.Name == p_Name
                            && tt.Active == true
                          select tt;

                // Return
                return qry.OrderBy(tt => tt.ID).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic Entity_Get(string p_DataSource)
        {
            using (alfaDS DS = new alfaDS())
            {
                switch (p_DataSource)
                {
                    case "TableGroup" : return DS.Context.TableGroup.Select(tt => new { tt.ID, tt.Name }).OrderBy(tt=> tt.ID).ToList();
                    case "IT_Users"   : return DS.Context.TableUser.Where(tt => tt.TableGroup.Name == "IT").Select(tt => new { tt.FullName }).OrderBy(tt => tt.FullName).ToList();

                    default: return null;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string ConnStr_DeCrypt()
        {
            try
            {
                // Config File
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Get ConnStr
                string p_ConnStr = cfgFile.ConnectionStrings.ConnectionStrings[alfaEntity.EntityObjectName].ConnectionString;

                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_ConnStr);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // DeCrypt Password
                sbSQL.Password = alfaSec.DeCrypt(sbSQL.Password);

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Return
                return sbENT.ConnectionString;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ConnStr_EnCrypt(string p_ConnStr)
        {
            try
            {
                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_ConnStr);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // EnCrypt Password
                sbSQL.Password = alfaSec.EnCrypt(sbSQL.Password);

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Config File
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Set Properties
                cfgFile.ConnectionStrings.ConnectionStrings[alfaEntity.EntityObjectName].ConnectionString = sbENT.ConnectionString;

                // Save Changes to File
                cfgFile.Save(ConfigurationSaveMode.Modified);

                // Force Reload
                ConfigurationManager.RefreshSection("connectionStrings");

                // Refresh
                alfaDS.m_ConnStr = null;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); 
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        
        public static string ConnStr_Test(string p_ServerName, out string p_Result)
        {
            try
            {
                // Get ConnString
                string p_EntityConnStr = alfaEntity.ConnStr_DeCrypt();

                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_EntityConnStr);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // Set ServerName
                sbSQL.DataSource = p_ServerName;

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Test Connection
                using( alfaDS ent = new alfaDS(sbENT.ConnectionString))
                {
                    // Wait
                    alfaMsg.CursorWait();

                    // Test
                    var qry = ent.Context.TableUser.ToList();

                    // Default
                    alfaMsg.CursorDefult();

                    // Message
                    p_Result = " SQL Server was Successfully Tested ...!";

                    // Pass
                    return sbENT.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                // Set Message
                p_Result = ex.Message;
                
                // Return
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Copy(Object p_Source, Object p_Target)
        {
            try
            {
                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties() )
	            {
                    // Check Name
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value
                    var newValue = prop.GetValue(p_Source, null);

                    // Check
                    if (p_Target.GetType().GetProperty(prop.Name) != null)
                    {
                        // Set Value
                        p_Target.GetType().GetProperty(prop.Name).SetValue(p_Target, newValue, null);
                    }
	            }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Check(Object p_Source, Object p_Target)
        {
            try
            { 
                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties() )
	            {
                    // Check
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value Source
                    var p_ValueSource = prop.GetValue(p_Source, null);

                    // Get Value Target
                    var p_ValueTarget = p_Target.GetType().GetProperty(prop.Name).GetValue(p_Target, null);

                    // Check1
                    if (p_ValueTarget == null && p_ValueSource == null) continue;

                    // Check2
                    else if (p_ValueTarget == null && p_ValueSource != null) return false;

                    // Check3
                    else if (p_ValueTarget != null && p_ValueSource == null) return false;

                    // Check3
                    if (!p_ValueSource.Equals(p_ValueTarget)) return false;
	            }

                // Return
                return true;
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int Report_TotalValues_V1(DateTime p_DT1, DateTime p_DT2, string p_ReqType)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Query
                    var qry = from tt in DS.Context.TableRequest
                              where tt.ReqStart >= p_DT1
                                 && tt.ReqStart <= p_DT2
                                 && tt.ReqType == p_ReqType
                              select tt;

                    // Return
                    return qry.Count();
                }
            }
            catch
            {
                // Error
                return 0;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int Report_TotalValues_V2(DateTime p_DT1, DateTime p_DT2, string p_ReqStatus)
        { 
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Query
                    var qry = from tt in DS.Context.TableRequest
                              where tt.ReqStart >= p_DT1
                                 && tt.ReqStart <= p_DT2
                                 && tt.USR_Request_01 == p_ReqStatus
                              select tt;

                    // Return
                    return qry.Count();
                }
            }
            catch
            {
                // Error
                return 0;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableSession> TableSession_GetList(DateTime p_DateStart, DateTime p_DateFinish)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableSession where tt.SessionStart >= p_DateStart && tt.SessionStart <= p_DateFinish select tt;

                // Return
                return qry.OrderBy(tt => tt.Status).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableSession_Add(alfaSession p_SessionInfo)
        {
            // Check Admin
            if (p_SessionInfo.User == "ADMIN") return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Create
                    TableSession p_Session = new TableSession();

                    // Set Properties
                    p_Session.SessionStart = DateTime.Now;
                    p_Session.SessionFinish = null;
                    p_Session.ElapsedTime = null;
                    p_Session.IPAddress = p_SessionInfo.LocIP;
                    p_Session.AppVersion = p_SessionInfo.AppVer;
                    p_Session.NetVersion = p_SessionInfo.NetVer;
                    p_Session.PCName = p_SessionInfo.PC;
                    p_Session.Status = alfaStr.ACTIVE;
                    p_Session.UserName = p_SessionInfo.UserGroup();

                    // Add
                    DS.Context.TableSession.Add(p_Session);

                    // Save
                    DS.Context.SaveChanges();

                    // Set ID
                    p_SessionInfo.SessionID = p_Session.ID;
                }
            }

            catch
            {
                // NULL
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableSession_Close(alfaSession p_SessionInfo)
        {
            // Check ID
            if (p_SessionInfo.SessionID == 0) return;

            // Check Admin
            if (p_SessionInfo.User == "ADMIN") return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Get Record
                    var p_Session = DS.Context.TableSession.Where(tt => tt.ID == p_SessionInfo.SessionID).Single();

                    // Set Properties
                    p_Session.SessionFinish = DateTime.Now;

                    // Set Properties
                    DateTime p_DateTime01 = Convert.ToDateTime(p_Session.SessionStart.Value);
                    DateTime p_DateTime02 = Convert.ToDateTime(p_Session.SessionFinish.Value);

                    // Set ElapsedTime
                    p_Session.ElapsedTime = Convert.ToInt64((p_DateTime02 - p_DateTime01).TotalSeconds);

                    // Close Session
                    p_Session.Status = alfaStr.CLOSED;

                    // Save
                    DS.Context.SaveChanges();
                }
            }

            catch
            {
                // NULL
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    # endregion 

}