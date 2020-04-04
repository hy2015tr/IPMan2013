using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Http;
using DevExpress.XtraGrid;
using DevExpress.XtraBars;
using System.Net.Security;
using System.Windows.Forms;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;



namespace IPMAN2013
{
    class alfaWAPI
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private string m_InfoBlox_Addr = "https://10.1.1.104"; // InfoBlox Address
        private string m_InfoBlox_User = "webapi";             // InfoBlox User
        private string m_InfoBlox_Pass = "123456";             // InfoBlox Pass
        private string m_InfoBlox_Vers = "/wapi/v1.1";         // InfoBlox Version
        private string m_PbnServer_Addr = "10.4.42.22";        // PbnServer Address
        private string m_PbnServer_User = "hasany";            // PbnServer User
        private string m_PbnServer_Pass = "start123";          // PbnServer Pass
        private string m_PbnServer_Vers = "v6.0";              // InfoBlox Version
        private NetworkCredential m_Credential = null;         // Credential
        private WebRequestHandler m_Handler = null;            // WebRequestHandler
        private HttpClient m_Client = null;                    // HttpClient
        private BarStaticItem m_Status = null;                 // Status
        public bool IsRunning { get; set; }                    // Runnig Process

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public string GetAddress()
        { 
            // Parameters
            this.GetParameters();

            // Return
            return (this.m_InfoBlox_Addr + this.m_InfoBlox_Vers);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public alfaWAPI(BarStaticItem p_Status)
        {
            try
            {
                // Set Status
                this.m_Status = p_Status;

                // Running
                this.SetRunnigStatus(false);

                // SSL Certificate Check 
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificates);

                // Parameters
                this.GetParameters();

                // Create Credential
                this.m_Credential = new NetworkCredential(this.m_InfoBlox_User, this.m_InfoBlox_Pass);

                // Create Handler
                this.m_Handler = new WebRequestHandler() { Credentials = m_Credential, UseProxy = true };

                // Create Client
                this.m_Client = new HttpClient(this.m_Handler);

                // Set Address
                this.m_Client.BaseAddress = new Uri(this.m_InfoBlox_Addr);

                // Add MediaType
                this.m_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void GetParameters()
        {
            // Get INFOBLOX
            var listServerV1 = alfaEntity.TableServer_GetList("INFOBLOX");

            if (listServerV1.Count > 0)
            {
                // Set Values
                this.m_InfoBlox_Addr = listServerV1[0].Address;
                this.m_InfoBlox_User = listServerV1[0].UserName;
                this.m_InfoBlox_Pass = listServerV1[0].Password;
                this.m_InfoBlox_Vers = listServerV1[0].Version;
            }

            // Get PBNSERVER
            var listServerV2 = alfaEntity.TableServer_GetList("PBNSERVER");

            if (listServerV2.Count > 0)
            {
                // Set Values
                this.m_PbnServer_Addr = listServerV2[0].Address;
                this.m_PbnServer_User = listServerV2[0].UserName;
                this.m_PbnServer_Pass = listServerV2[0].Password;
                this.m_PbnServer_Vers = listServerV2[0].Version;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SetRunnigStatus(bool p_Status)
        {
            // Running
            this.IsRunning = p_Status;

            if (p_Status)
            {
                // Set Message
                alfaCtrl.SetText(this.m_Status, "WAPI : Running ...", true);
            }
            else
            {
                // Set Message
                alfaCtrl.SetText(this.m_Status, "WAPI : OK", true);
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        static private bool ValidateCertificates(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // Always Return True
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public async void GetSubnetList(GridView p_GridView, string p_Site, string p_Count, string p_Network)
        {
            try
            {
                // Running
                this.SetRunnigStatus(true);

                // Parameters
                this.GetParameters();

                // Request String
                string reqStr = String.Format("{0}/network?*Site:={1}&_max_results={2}&_return_fields=comment,network,extensible_attributes&network~={3}", this.m_InfoBlox_Vers, p_Site, p_Count, p_Network);

                // Get Response ( WAIT and NOT BLOCK )
                HttpResponseMessage resp = await this.m_Client.GetAsync(reqStr);

                // Get String
                string strGET = resp.Content.ReadAsStringAsync().Result;

                // Get JArray
                var obj = JArray.Parse(strGET);

                // Create List
                List<rowSubnet> listSubnet = new List<rowSubnet>();

                // Loop in JObjects
                foreach (var token in obj.Children())
                {
                    // Create Row
                    rowSubnet row = new rowSubnet();

                    // Set Main Properties
                    row.Network = Convert.ToString(token.SelectToken("network"));
                    row.Comment = Convert.ToString(token.SelectToken("comment"));

                    // Get Enxtensible Properties
                    JToken jExt = token.SelectToken("extensible_attributes");

                    if (jExt != null)
                    {
                        // Set Enxtensible Properties
                        row.ExtServerName = Convert.ToString(jExt.SelectToken("ServerName"));
                        row.ExtModulePort1 = Convert.ToString(jExt.SelectToken("ModulePort1"));
                        row.ExtModulePort2 = Convert.ToString(jExt.SelectToken("ModulePort2"));
                        row.ExtNote = Convert.ToString(jExt.SelectToken("NOTE"));
                        row.ExtResponsible = Convert.ToString(jExt.SelectToken("Responsible"));
                        row.ExtSite = Convert.ToString(jExt.SelectToken("Site"));
                        row.ExtSwitch1 = Convert.ToString(jExt.SelectToken("Switch1"));
                        row.ExtSwitch2 = Convert.ToString(jExt.SelectToken("Switch2"));
                        row.ExtVlanNo = Convert.ToString(jExt.SelectToken("VlanNo"));
                    }

                    // Add List
                    listSubnet.Add(row);
                }

                // Set GridView
                p_GridView.Columns.Clear();
                p_GridView.GridControl.DataSource = listSubnet.ToList();
                alfaGrid.SetView(p_GridView, false);

                // Running
                this.SetRunnigStatus(false);
            }

            catch (Exception ex)
            {
                // Running
                this.IsRunning = false;

                // Set Message
                alfaCtrl.SetText(this.m_Status, "ERROR : " + ex.Message, false);

                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public async void GetIPv4List( GridView p_GridView, string p_IPAddress, string p_Count, string p_Status )
        {
            try
            {
                // Running
                this.SetRunnigStatus(true);

                // Parameters
                this.GetParameters();

                // Get Position (217.174.43.32/29)  (10.2012.4.0/24)
                int pos = p_IPAddress.LastIndexOf(".");

                //HY Replace with 10
                //HY p_IPAddress = p_IPAddress.Substring(0, pos) + ".10" ;

                // Request String
                string reqStr = String.Format("{0}/ipv4address?ip_address>={1}&_max_results={2}&status={3}&_return_fields=network,extensible_attributes,ip_address,is_conflict,mac_address,status", this.m_InfoBlox_Vers, p_IPAddress, p_Count, p_Status);

                // Get Response ( WAIT and NOT BLOCK )
                HttpResponseMessage resp = await this.m_Client.GetAsync(reqStr);

                // Get String
                string strGET = resp.Content.ReadAsStringAsync().Result;

                // Get JArray
                var obj = JArray.Parse(strGET);

                // Create List
                List<rowIPv4> listSubnet = new List<rowIPv4>();

                // Loop in JObjects
                foreach (var token in obj.Children())
                {
                    // Create Row
                    rowIPv4 row = new rowIPv4();

                    // Set Main Properties
                    row.Network = Convert.ToString(token.SelectToken("network"));
                    row.IPAddress = Convert.ToString(token.SelectToken("ip_address"));
                    row.IsConflict = Convert.ToString(token.SelectToken("is_conflict"));
                    row.MacAddress = Convert.ToString(token.SelectToken("mac_address"));
                    row.IPStatus = Convert.ToString(token.SelectToken("status"));
                    row.ExtPortSecurity = true;

                    // Get Enxtensible Properties
                    JToken jExt = token.SelectToken("extensible_attributes");

                    if (jExt != null)
                    {
                        // Set Enxtensible Properties
                        row.ExtServerName = Convert.ToString(jExt.SelectToken("ServerName"));
                        row.ExtModulePort1 = Convert.ToString(jExt.SelectToken("ModulePort1"));
                        row.ExtModulePort2 = Convert.ToString(jExt.SelectToken("ModulePort2"));
                        row.ExtNote = Convert.ToString(jExt.SelectToken("NOTE"));
                        row.ExtResponsible = Convert.ToString(jExt.SelectToken("Responsible"));
                        row.ExtSite = Convert.ToString(jExt.SelectToken("Site"));
                        row.ExtSwitch1 = Convert.ToString(jExt.SelectToken("Switch1"));
                        row.ExtSwitch2 = Convert.ToString(jExt.SelectToken("Switch2"));
                        row.ExtVlanNo = Convert.ToString(jExt.SelectToken("VlanNo"));
                    }

                    // Add List
                    listSubnet.Add(row);
                }

                foreach (var row in listSubnet.ToList())
                {
                    // Set Request Status
                    if (alfaEntity.TableRequest_CheckStatus(row.IPAddress)) row.ReqStatus = alfaStr.PROCESSING;
                }

                // Set GridView
                p_GridView.Columns.Clear();
                p_GridView.GridControl.DataSource = listSubnet.ToList();
                alfaGrid.SetView(p_GridView, false);

                // Running
                this.SetRunnigStatus(false);
            }

            catch (Exception ex)
            {
                // Running
                this.IsRunning = false;

                // Set Message
                alfaCtrl.SetText(this.m_Status, "ERROR : " + ex.Message, false);

                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void UpdateIPv4List_SSH(List<TableRequest> p_List, alfaSession p_Session)
        {
            try
            {
                // Running
                this.SetRunnigStatus(true);

                // Parameters
                this.GetParameters();

                // SSH Client
                Renci.SshNet.SshClient sshClient = new Renci.SshNet.SshClient(this.m_PbnServer_Addr, this.m_PbnServer_User, this.m_PbnServer_Pass);

                // Connect
                sshClient.Connect();

                foreach (TableRequest p_Req in p_List)
                {
                    // Command String
                    string p_Cmd = string.Empty;


                    if ("IP_DEL".Contains(p_Req.ReqType))  // DELETE
                    {
                        // Set Command
                        p_Cmd = string.Format(" perl infoblox_delete.pl {0} ", alfaStr.SetDefault(p_Req.IPAddress));

                    }
                    else if ("IP_EDIT-IP_NEW".Contains(p_Req.ReqType))  // EDIT + DELETE
                    {
                        // Set Command
                        p_Cmd = string.Format(" perl infoblox_update.pl \"{0}\"  \"{1}\"  \"{2}\"  \"{3}\"  \"{4}\"  \"{5}\"  \"{6}\"  \"{7}\"  \"{8}\"  \"{9}\""
                                                      , alfaStr.SetDefault(p_Req.IPAddress)
                                                      , alfaStr.SetDefault(p_Req.ExtSite)
                                                      , alfaStr.SetDefault(p_Req.ExtNote)
                                                      , alfaStr.SetDefault(p_Req.ExtServerName)
                                                      , alfaStr.SetDefault(p_Req.ExtResponsible)
                                                      , alfaStr.SetDefault(p_Req.ExtModulePort1)
                                                      , alfaStr.SetDefault(p_Req.ExtModulePort2)
                                                      , alfaStr.SetDefault(p_Req.ExtSwitch1)
                                                      , alfaStr.SetDefault(p_Req.ExtSwitch2)
                                                      , alfaStr.SetDefault(p_Req.ExtVlanNo));
                    }

                    // Create Command
                    var cmdUnix = sshClient.CreateCommand(alfaStr.GetEnglishText(p_Cmd));

                    //HY
                    Application.DoEvents();

                    // Get Result
                    string p_Result = cmdUnix.Execute() + cmdUnix.Error;

                    // Set Result
                    if (p_Result.Contains("INFOBLOX_UPDATE_OK")  || p_Result.Contains("INFOBLOX_DELETE_OK") ) 
                        
                        p_Req.PBN_Close_04 = alfaStr.OKEY; else p_Req.PBN_Close_04 = alfaStr.ERROR;

                    // Add Log
                    alfaLog.Add(p_Req, p_Session, "InfoBlox --> " + p_Result);

                    // Finish DateTime
                    p_Req.ReqFinish = DateTime.Now;

                    // Update Request
                    alfaEntity.TableRequest_Update(p_Req);
                }

                // Running
                this.SetRunnigStatus(false);

            }

            catch (Exception ex)
            {
                // Running
                this.IsRunning = false;

                // Set Message
                alfaCtrl.SetText(this.m_Status, "ERROR : " + ex.Message, false);

                // Show Error
                alfaMsg.Error(ex);
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void PingIPv4List_SSH(List<TableRequest> p_List, alfaSession p_Session, GridView p_GridView)
        { 
            try
            {
                // Running
                this.SetRunnigStatus(true);

                // Parameters
                this.GetParameters();

                // SSH Client
                Renci.SshNet.SshClient sshClient = new Renci.SshNet.SshClient(this.m_PbnServer_Addr, this.m_PbnServer_User, this.m_PbnServer_Pass);

                // Connect
                sshClient.Connect();

                foreach (TableRequest p_Req in p_List)
                {
                    // Create String
                    string p_Cmd = string.Format(" perl infoblox_ping.pl \"{0}\" ", alfaStr.SetDefault(p_Req.IPAddress));

                    // Create Command
                    var cmdUnix = sshClient.CreateCommand(p_Cmd);

                    //HY
                    Application.DoEvents();

                    // Get Result
                    string p_Result = cmdUnix.Execute() + cmdUnix.Error;

                    // Set Result
                    if (p_Result.Contains("INFOBLOX_PING_OK")) p_Req.PingStatus = alfaStr.PING_REPLIED;
                    else if (p_Result.Contains("INFOBLOX_PING_ERROR")) p_Req.PingStatus = alfaStr.PING_TIMEOUT;
                    else p_Req.PingStatus = alfaStr.ERROR;

                    // Clear Check
                    p_Req.Check = false;

                    // Add Log
                    alfaLog.Add(p_Req, p_Session, "InfoBlox --> " + p_Result);

                    // Update Request
                    alfaEntity.TableRequest_Update(p_Req);

                    // Refresh
                    p_GridView.RefreshData();

                    // Do Events
                    Application.DoEvents();
                }

                // Running
                this.SetRunnigStatus(false);
            }

            catch (Exception ex)
            {
                // Running
                this.IsRunning = false;

                // Set Message
                alfaCtrl.SetText(this.m_Status, "ERROR : " + ex.Message, false);

                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void PingIPv4List_SSH(List<rowIPv4> p_List, alfaSession p_Session, GridView p_GridView)
        { 
            try
            {
                // Running
                this.SetRunnigStatus(true);

                // Parameters
                this.GetParameters();

                // SSH Client
                Renci.SshNet.SshClient sshClient = new Renci.SshNet.SshClient(this.m_PbnServer_Addr, this.m_PbnServer_User, this.m_PbnServer_Pass);

                // Connect
                sshClient.Connect();

                foreach (rowIPv4 p_Req in p_List)
                {
                    // Create String
                    string p_Cmd = string.Format(" perl infoblox_ping.pl \"{0}\" ", alfaStr.SetDefault(p_Req.IPAddress));

                    // Create Command
                    var cmdUnix = sshClient.CreateCommand(p_Cmd);

                    // Get Result
                    string p_Result = cmdUnix.Execute() + cmdUnix.Error;

                    // Set Result
                         if (p_Result.Contains("INFOBLOX_PING_OK"))    p_Req.PingStatus = alfaStr.PING_REPLIED; 
                    else if (p_Result.Contains("INFOBLOX_PING_ERROR")) p_Req.PingStatus = alfaStr.PING_TIMEOUT;
                    else p_Req.PingStatus = alfaStr.ERROR;

                    // Clear Check
                    p_Req.Check = false;

                    // Refresh
                    p_GridView.RefreshData();

                    // Do Events
                    Application.DoEvents();
                }

                // Running
                this.SetRunnigStatus(false);
            }

            catch (Exception ex)
            {
                // Running
                this.IsRunning = false;

                // Set Message
                alfaCtrl.SetText(this.m_Status, "ERROR : " + ex.Message, false);

                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }
}
