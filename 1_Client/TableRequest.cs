//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPMAN2013
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableRequest
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public Nullable<System.DateTime> ReqStart { get; set; }
        public Nullable<System.DateTime> ReqFinish { get; set; }
        public string ReqUser { get; set; }
        public string ReqLog { get; set; }
        public string ReqType { get; set; }
        public string USR_Request_01 { get; set; }
        public string PBN_Apprv_02 { get; set; }
        public string CBL_Switch_03 { get; set; }
        public string PBN_Close_04 { get; set; }
        public string PingStatus { get; set; }
        public string IPStatus { get; set; }
        public string IPAddress { get; set; }
        public string Network { get; set; }
        public string IsConflict { get; set; }
        public string MacAddress { get; set; }
        public string ExtPriority { get; set; }
        public string ExtSite { get; set; }
        public string ExtNote { get; set; }
        public string ExtServerName { get; set; }
        public string ExtResponsible { get; set; }
        public Nullable<bool> ExtVM { get; set; }
        public Nullable<bool> ExtBackup { get; set; }
        public Nullable<bool> ExtPortSecurity { get; set; }
        public string ExtModulePort1 { get; set; }
        public string ExtModulePort2 { get; set; }
        public string ExtSwitch1 { get; set; }
        public string ExtSwitch2 { get; set; }
        public string ExtVlanNo { get; set; }
    }
}
