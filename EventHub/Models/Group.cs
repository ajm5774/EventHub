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
    
    public partial class Group
    {
        public Group()
        {
            this.Events = new HashSet<Event>();
            this.GroupSubscriptions = new HashSet<GroupSubscription>();
            this.AdminRequests = new HashSet<AdminRequest>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public int SchoolId { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<GroupSubscription> GroupSubscriptions { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<AdminRequest> AdminRequests { get; set; }
    }
}
