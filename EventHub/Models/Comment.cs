//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHub.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int EventId { get; set; }
        public string AspNetUserId { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
