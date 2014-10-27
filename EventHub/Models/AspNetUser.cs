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
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetUser :IdentityUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.Notifications = new HashSet<Notification>();
            this.GroupSubscriptions = new HashSet<GroupSubscription>();
            this.UserEventNotifications = new HashSet<UserEventNotification>();
            this.Comments = new HashSet<Comment>();
            this.AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }
        public Nullable<int> SchoolId { get; set; }
    
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<GroupSubscription> GroupSubscriptions { get; set; }
        public virtual ICollection<UserEventNotification> UserEventNotifications { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
