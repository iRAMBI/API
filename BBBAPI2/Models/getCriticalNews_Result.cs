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
    
    public partial class getCriticalNews_Result
    {
        public int newsid { get; set; }
        public string userid { get; set; }
        public int programid { get; set; }
        public int coursesectionid { get; set; }
        public System.DateTime datetime { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string priority { get; set; }
        public System.DateTime expirydate { get; set; }
        public bool active { get; set; }
    }
}