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
    
    public partial class Faculty
    {
        public Faculty()
        {
            this.Courses = new HashSet<Course>();
            this.Programs = new HashSet<Program>();
            this.Teachers = new HashSet<Teacher>();
        }
    
        public int facultyid { get; set; }
        public string facultyname { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
