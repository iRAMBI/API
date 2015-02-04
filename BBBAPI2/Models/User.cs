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
    
    public partial class User
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.News = new HashSet<News>();
            this.Teachers = new HashSet<Teacher>();
            this.UserCourseSections = new HashSet<UserCourseSection>();
        }
    
        public string userid { get; set; }
        public Nullable<int> programid { get; set; }
        public Nullable<int> setid { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public bool active { get; set; }
        public string phonenumber { get; set; }
        public string token { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual Program Program { get; set; }
        public virtual Set Set { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<UserCourseSection> UserCourseSections { get; set; }
    }
}
