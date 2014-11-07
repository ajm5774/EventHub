﻿using System;
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
                db.Comments.Add(comment);
                db.SaveChanges();

                NotificationController nController = new NotificationController();
                Notification notification = new Notification();
                notification.Message = User.Identity.Name + " has sent the message " + comment.Message;
                notification.NotificationType = 0;
                notification.AspNetUserId = User.Identity.GetUserId();

                var prevComments = GetPrevComments(comment.EventId);
                foreach(Comment c in prevComments){
                    if(c.AspNetUserId != User.Identity.GetUserId()){
                        notification.AspNetUserId1 = c.AspNetUserId;
                        nController.Create(notification);
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
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