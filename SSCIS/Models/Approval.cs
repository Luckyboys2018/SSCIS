//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSCIS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Approval
    {
        public int ID { get; set; }
        public int TutorID { get; set; }
        public int SubjectID { get; set; }
    
        public virtual Subject Subject { get; set; }
        public virtual SSCISUser Tutor { get; set; }
    }
}