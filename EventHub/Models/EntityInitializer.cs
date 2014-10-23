using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class EntityInitializer : DropCreateDatabaseAlways<Entities>
    {
        protected override void Seed(Entities context)
        {
            try
            {
                //schools - need this first, users depend on it
                new List<School>
                {
                    new School() { Id = 1, Name = "Rochester Institute of Technology" },
                    new School() { Id = 2, Name = "University of Rochester" },
                    new School() { Id = 3, Name = "Harvard University" }
                }.ForEach(school => context.Schools.Add(school));
                context.SaveChanges();

                //Groups
                new List<Group>
                {
                    new Group() { Id = 1, Name = "Outting Club", Description = "People get together to do outdoor things.", PicturePath = "/group1Pic"},
                    new Group() { Id = 2, Name = "Dodgeball Club", Description = "Dodge Dip Dive and Dodge", PicturePath = "/group2Pic" }
                }.ForEach(group => context.Groups.Add(group));
                context.SaveChanges();

                //users
                ApplicationDbContext idContext = new ApplicationDbContext();//we need a different context for users
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(idContext));
                var userValidator = manager.UserValidator as UserValidator<ApplicationUser>;
                userValidator.AllowOnlyAlphanumericUserNames = false;
                var user1 = new ApplicationUser { Id = "1", FirstName = "Bob", LastName = "Hope", UserName = "bHope@gmail.com", SchoolId = 1, PicturePath = "/bHope.jpeg" };
                var user2 = new ApplicationUser { Id = "2", FirstName = "Bobby", LastName = "Joe", UserName = "bJoe@gmail.com", SchoolId = 1, PicturePath = "/bJoe.jpeg" };
                manager.Create(user1, "ChangeItAsap!");
                manager.Create(user2, "ChangeItAsap!");

                //Events
                new List<Event>
                {
                    new Event() {
                        Id = 1,
                        DateTime = DateTime.Now.AddDays(20),
                        GroupId = 1,
                        Place = "Red Barn",
                        Title = "Rock Climbing Trip",
                        Description = "We are going rock climbing at some awesome place, join us!" },
                    new Event() {
                        Id = 2,
                        DateTime = DateTime.Now.AddDays(-10),
                        GroupId = 2,
                        Place = "Clark Gym",
                        Title = "Dodgeball Tournament",
                        Description = "This tourny is gonna be awesome. Have teams of 6 register an hour before the event!" },
                    new Event() {
                        Id = 3,
                        DateTime = DateTime.Now.AddDays(10),
                        GroupId = 1,
                        Place = "Red Barn 1",
                        Title = "Rock Climbing Trip 1",
                        Description = "We are going rock climbing at some awesome place, join us, again!" },
                    new Event() {
                        Id = 4,
                        DateTime = DateTime.Now.AddDays(5),
                        GroupId = 2,
                        Place = "Clark Gym 1",
                        Title = "Dodgeball Tournament 1",
                        Description = "This tourny is gonna be awesome. Have teams of 6 register an hour before the event!" },
                }.ForEach(e => context.Events.Add(e));
                context.SaveChanges();

                //Comment
                new List<Comment>
                {
                    new Comment() { Id = 1, EventId = 1, Message = "TeeHee This event looks totes cool!", AspNetUserId = "1"},
                    new Comment() { Id = 2, EventId = 1, Message = "Aww, ive got something else planned!", AspNetUserId = "2" }
                }.ForEach(comment => context.Comments.Add(comment));
                context.SaveChanges();

                //EventPictures
                new List<EventPicture>
                {
                    new EventPicture() { Id = 1, EventId = 1, PicturePath = "/testEventPicture.jpg" }
                }.ForEach(eventPicture => context.EventPictures.Add(eventPicture));
                context.SaveChanges();

                //GroupSubscriptions
                new List<GroupSubscription>
                {
                    new GroupSubscription() { Id = 1, IsAdministrator = true, AspNetUserId="1", GroupId = 1 },
                    new GroupSubscription() { Id = 2, IsAdministrator = false, AspNetUserId = "2", GroupId = 1 }
                }.ForEach(groupSubscription => context.GroupSubscriptions.Add(groupSubscription));
                context.SaveChanges();

                //UserEventNotifications
                new List<UserEventNotification>
                {
                    new UserEventNotification() { Id = 1, AllowNotifications = true, AspNetUsersId = "1", EventsId = 1 },
                    new UserEventNotification() { Id = 2, AllowNotifications = false, AspNetUsersId = "2", EventsId = 1 }
                }.ForEach(userEventNotification => context.UserEventNotifications.Add(userEventNotification));
                context.SaveChanges();
                
                //Notifications
                new List<Notification>
                {
                    new Notification() { Id = 1, Message = "Someone event changed.", AspNetUserId = "2", AspNetUserId1 = "1", NotificationType = NotificationType.EventChanged, Viewed=false },
                    new Notification() { Id = 2, Message = "Someone Commented on an event youre in.", AspNetUserId = "1", AspNetUserId1 = "2", NotificationType = NotificationType.Comment, Viewed=false},
                    new Notification() { Id = 2, Message = "Someone1 Commented on an event youre in.", AspNetUserId = "1", AspNetUserId1 = "2", NotificationType = NotificationType.Comment, Viewed=true}
                }.ForEach(notification => context.Notifications.Add(notification));
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach(DbEntityValidationResult res in ex.EntityValidationErrors)
                    Console.WriteLine(res.ToString());
            }

            base.Seed(context);
        }
    }
}