//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BBBAPI2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        public int facultyid { get; set; }
        public string userid { get; set; }
        public string position { get; set; }
        public string alternateemail { get; set; }
        public string officelocation { get; set; }
        public string ohmonday { get; set; }
        public string ohtuesday { get; set; }
        public string ohwednesday { get; set; }
        public string ohthursday { get; set; }
        public string ohfriday { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual User User { get; set; }
    }
}
