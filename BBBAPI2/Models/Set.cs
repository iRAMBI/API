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
    
    public partial class Set
    {
        public Set()
        {
            this.Users = new HashSet<User>();
        }
    
        public int setid { get; set; }
        public int programid { get; set; }
        public string setname { get; set; }
    
        public virtual Program Program { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}