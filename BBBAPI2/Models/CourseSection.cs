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
    
    public partial class CourseSection
    {
        public int coursesectionid { get; set; }
        public int courseid { get; set; }
        public Nullable<System.DateTime> datetimestart { get; set; }
        public Nullable<System.DateTime> datetimeend { get; set; }
    
        public virtual Course Course { get; set; }
    }
}
