﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<EventPicture> EventPictures { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupSubscription> GroupSubscriptions { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<UserEventNotification> UserEventNotifications { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<EventUserReply> EventUserReplies { get; set; }
    }
}
