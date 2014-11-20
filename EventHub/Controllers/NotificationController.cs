using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;

namespace EventHub.Controllers
{
    public class NotificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Notification/
        public ActionResult Index()
        {
            var notifications = db.Notifications.Include(n => n.AspNetUser).Include(n => n.SourceUser);
            return View(notifications.ToList());
        }

        public PartialViewResult NotificationFeed()
        {
            var id = User.Identity.GetUserId();
            List<Notification> notifications = db.Notifications.Where(i =>
                (i.AspNetUserId1 == id) &&
                (i.Viewed == false)
            ).ToList();
            return PartialView(notifications);
        }

        // GET: /Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: /Notification/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.AspNetUserId1 = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: /Notification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Message,Viewed,NotificationType,AspNetUserId,AspNetUserId1")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId);
            ViewBag.AspNetUserId1 = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId1);
            return View(notification);
        }

        // GET: /Notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId);
            ViewBag.AspNetUserId1 = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId1);
            return View(notification);
        }

        // POST: /Notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Message,Viewed,NotificationType,AspNetUserId,AspNetUserId1")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId);
            ViewBag.AspNetUserId1 = new SelectList(db.AspNetUsers, "Id", "UserName", notification.AspNetUserId1);
            return View(notification);
        }

        // GET: /Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: /Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
