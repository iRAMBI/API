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
    
    public partial class News
    {
        public News()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int newsid { get; set; }
        public int userid { get; set; }
        public System.DateTime datetime { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string priority { get; set; }
        public bool active { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
