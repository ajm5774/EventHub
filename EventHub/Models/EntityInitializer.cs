using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class EntityInitializer : DropCreateDatabaseAlways<Entities>
    {
        protected override void Seed(Entities context)
        {
            string wanted_path = System.AppDomain.CurrentDomain.BaseDirectory;
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
                    new Group() { Id = 1, Name = "Outting Club", Description = "People get together to do outdoor things.", PicturePath = "\\Content\\Images\\testImages\\group1Pic.jpg" , SchoolId = 1},
                    new Group() { Id = 2, Name = "Dodgeball Club", Description = "Dodge Dip Dive and Dodge", PicturePath = "\\Content\\Images\\testImages\\group2Pic.jpg", SchoolId = 1 }
                }.ForEach(group => context.Groups.Add(group));
                context.SaveChanges();

                //Events
                List<Event> events = new List<Event>
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
                };

                for (int i = 0; i < 25; i++ )
                {
                    int id = i+1;
                    events.Add(new Event()
                    {
                        Id = id+3,
                        DateTime = DateTime.Now.AddDays(id),
                        GroupId = 2,
                        Place = "Clark Gym 1",
                        Title = "Dodgeball Tournament " + id,
                        Description = "This tourny is gonna be awesome. Have teams of 6 register an hour before the event!"
                    });
                }

                events.ForEach(e => context.Events.Add(e));
                context.SaveChanges();

                //Roles
                ApplicationDbContext identityContext = new ApplicationDbContext();
                //var store = new RoleStore<AspNetRole>(identityContext);
                //var roleManager = new RoleManager<AspNetRole>(store);
                //var role = new AspNetRole { Name = "BasicUser" };
                //roleManager.Create(role);
                //identityContext.SaveChanges();

                //user
                var manager = new UserManager<AspNetUser>(new UserStore<AspNetUser>(identityContext));
                var userValidator = manager.UserValidator as UserValidator<AspNetUser>;
                userValidator.AllowOnlyAlphanumericUserNames = false;
                var user1 = new AspNetUser { Id = "1", FirstName = "Bob", LastName = "Hope", UserName = "bHope@gmail.com", SchoolId = 1, PicturePath = "\\Content\\Images\\testImages\\bob-hope.jpg" };
                var user2 = new AspNetUser { Id = "2", FirstName = "Bobby", LastName = "Joe", UserName = "bJoe@gmail.com", SchoolId = 1, PicturePath = "\\Content\\Images\\testImages\\bobby-joe.jpg" };
                manager.Create(user1, "ChangeItAsap!");
                //manager.AddToRole(user1.Id, "BasicUser");
                manager.Create(user2, "ChangeItAsap!");
                //manager.AddToRole(user2.Id, "BasicUser");
                identityContext.SaveChanges();

                //Comment
                new List<Comment>
                {
                    new Comment() { Id = 1, EventId = 1, Message = "TeeHee This event looks totes cool!", AspNetUserId = "1", DateTime=DateTime.Now},
                    new Comment() { Id = 2, EventId = 1, Message = "Aww, ive got something else planned!", AspNetUserId = "2", DateTime=DateTime.Now }
                }.ForEach(comment => context.Comments.Add(comment));
                context.SaveChanges();

                //EventPictures
                new List<EventPicture>
                {
                    new EventPicture() { Id = 1, EventId = 1, PicturePath = "\\Content\\Images\\testImages\\group_default.png" }
                }.ForEach(eventPicture => context.EventPictures.Add(eventPicture));
                context.SaveChanges();

                //GroupSubscriptions
                new List<GroupSubscription>
                {
                    new GroupSubscription() { Id = 1, IsAdministrator = true, AspNetUserId="1", GroupId = 1 },
                    new GroupSubscription() { Id = 2, IsAdministrator = false, AspNetUserId="1", GroupId = 2 },
                    new GroupSubscription() { Id = 3, IsAdministrator = false, AspNetUserId = "2", GroupId = 1 }
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