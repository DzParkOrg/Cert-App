//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeCertApp_WebAPi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Designation
    {
        public int DesignationID { get; set; }
        public string DesignationTitle { get; set; }
        public int CreatedByUserId { get; set; }
        public System.DateTime CreationTimeStamp { get; set; }
        public int LastModdifiedbyUserId { get; set; }
        public System.DateTime LastModdifiedTimeStamp { get; set; }
    }
}
