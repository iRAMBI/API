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
    
    public partial class Course
    {
        public Course()
        {
            this.CourseSections = new HashSet<CourseSection>();
        }
    
        public int courseid { get; set; }
        public int facultyid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<CourseSection> CourseSections { get; set; }
    }
}
