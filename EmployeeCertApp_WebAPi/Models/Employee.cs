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
    
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DesignationId { get; set; }
        public int CertificationCategoryId { get; set; }
        public string EmployeeBand { get; set; }
        public string EMailId { get; set; }
        public string MobileNo { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public Nullable<bool> IsApprover { get; set; }
        public int CreatedByUserId { get; set; }
        public System.DateTime CreationTimeStamp { get; set; }
        public int LastModifiedbyUserId { get; set; }
        public System.DateTime LastModifiedTimeStamp { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
    }
}
