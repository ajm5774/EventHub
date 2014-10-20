using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public partial class Entities : DbContext
    {
        public void Seed(Entities Context)
        {
            //schools
            var school1 = new School() { Id = 1, Name = "Rochester Institute of Technology" };
            var school2 = new School() { Id = 2, Name = "University of Rochester" };
            var school3 = new School() { Id = 3, Name = "Harvard University" };
            //Groups
            var group1 = new Group() { Id = 1, Name = "Outting Club", Description = "People get together to do outdoor things."};
            var group2 = new Group() { Id = 2, Name = "Dodgeball Club", Description = "Dodge Dip Dive and Dodge" };
            //Events
            var event1 = new Event() {
                Id = 1,
                DateTime = DateTime.Now.AddDays(20),
                GroupId = 1,
                Place = "Red Barn",
                Title = "Rock Climbing Trip",
                Description = "We are going rock climbing at some awesome place, join us!" };
            //Comment
            var comment1 = new Comment() { Id = 1, EventId = 1, Message = "TeeHee This event looks totes cool!", UserId = 1 };
            //EventPictures
            //GroupSubscriptions
            //UserEventNotifications
            //var TestMyClass = new MyClass () { ... };
            //Context.MyClasses.Add (TestMyClass);

            // Normal seeding goes here

            //Context.SaveChanges ();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<Entities>
        {
            protected override void Seed(Entities context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }
    }
}