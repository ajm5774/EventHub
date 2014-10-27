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
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
	}
}