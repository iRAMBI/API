﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class irambidbEntities : DbContext
    {
        public irambidbEntities()
            : base("name=irambidbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSection> CourseSections { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCourseSection> UserCourseSections { get; set; }
    
        public virtual ObjectResult<validateToken_Result> validateToken(string userid, string token)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            var tokenParameter = token != null ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<validateToken_Result>("validateToken", useridParameter, tokenParameter);
        }
    
        public virtual ObjectResult<getCriticalNews_Result> getCriticalNews()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCriticalNews_Result>("getCriticalNews");
        }
    }
}
