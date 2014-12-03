using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;

namespace EventHub.Controllers
{
    public class CommentController : Controller
    {
        Entities db = new Entities();
        //
        // POST: /Event/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Comment comment = new Comment();
                comment.AspNetUserId = User.Identity.GetUserId();
                comment.Message = collection.Get("Message");
                comment.EventId = Int32.Parse(collection.Get("EventId"));
                comment.DateTime = DateTime.Now;
                db.Comments.Add(comment);
                

                Notification notification = new Notification();
                notification.Message = User.Identity.Name + " has sent the message \"" + comment.Message + "\"";
                notification.NotificationType = NotificationType.Comment;
                notification.AspNetUserId = User.Identity.GetUserId();

                HashSet<string> commentUserSet = new HashSet<string>();
                GetPrevComments(comment.EventId).ForEach(c => commentUserSet.Add(c.AspNetUserId));

                foreach (string userid in commentUserSet)
                {
                    if (userid != User.Identity.GetUserId())
                    {
                        //check if the user turned off notifications for the event
                        if (!db.UserEventNotifications.Where(uen =>
                            uen.AspNetUsersId == userid &&
                            uen.EventsId == comment.EventId &&
                            !uen.AllowNotifications).Any())
                        {
                            notification.AspNetUserId1 = userid;
                            db.Notifications.Add(notification);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<Comment> GetPrevComments(int eventId)
        {
            return db.Comments.Where(i => i.EventId == eventId).ToList();
        }

        public ActionResult EventCreate(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int thisId = Int32.Parse(collection.Get("EventId"));
                var events = db.Events.Single(a => a.Id == thisId);
                Comment comment = new Comment();
                comment.AspNetUserId = User.Identity.GetUserId();
                comment.Message = collection.Get("Message");
                comment.EventId = Int32.Parse(collection.Get("EventId"));
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "Event", new { id = events.Id });
            }
            catch
            {
                return View();
            }
        }
	}
}