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
    
    public partial class Certification
    {
        public int CertificationId { get; set; }
        public string CertificationCode { get; set; }
        public string CertificationTitle { get; set; }
        public string Description { get; set; }
        public int CertificationCategoryId { get; set; }
        public int CreatedByUserId { get; set; }
        public System.DateTime CreationTS { get; set; }
        public int LastModdifiedbyUserId { get; set; }
        public System.DateTime LastModdifiedTS { get; set; }
    }
}
