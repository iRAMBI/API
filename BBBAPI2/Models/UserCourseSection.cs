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
    
    public partial class UserCourseSection
    {
        public int coursesectionid { get; set; }
        public int userid { get; set; }
        public string role { get; set; }
    
        public virtual User User { get; set; }
    }
}