using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace EventHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext()
            : base("Entities")
        {
        }

        public System.Data.Entity.DbSet<EventHub.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<EventHub.Models.AspNetUser> AspNetUsers { get; set; }
    }
}