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
    
    public partial class Event
    {
        public Event()
        {
            this.Comments = new HashSet<Comment>();
            this.EventPictures = new HashSet<EventPicture>();
            this.UserEventNotifications = new HashSet<UserEventNotification>();
        }
    
        public int Id { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Place { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventPicture> EventPictures { get; set; }
        public virtual ICollection<UserEventNotification> UserEventNotifications { get; set; }
        public virtual Group Group { get; set; }
    }
}
