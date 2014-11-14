using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventHub.Controllers
{
    public class EventController : Controller
    {
        UserManager<AspNetUser> userManager;
        ApplicationDbContext authDb = new ApplicationDbContext();
        Entities db = new Entities();

        public EventController()
        {
            userManager = new UserManager<AspNetUser>(new UserStore<AspNetUser>(authDb));
        }

        [Authorize]
        public PartialViewResult EventFeed()
        {
            List<Event> events = new List<Event>();

            var id = User.Identity.GetUserId();
            var eventGroups = from gs in db.GroupSubscriptions.Where(s => s.AspNetUserId == id).ToList()
                             join g in db.Groups on gs.GroupId equals g.Id
                             select new
                             {
                                 Events = g.Events
                             };
            var eventGroupsList = eventGroups.ToList();

            foreach(var eventGroup in eventGroupsList)
            {
                events.AddRange(eventGroup.Events.Where(e => e.DateTime > DateTime.Now).ToList());
            }
            events = events.OrderBy(e => e.DateTime).ToList();
            return PartialView(events);
        }

        public PartialViewResult GroupEventFeed(int id)
        {
            List<Event> events = new List<Event>();

            var egroup = db.Groups.Where(g => g.Id == id).Single();
            events.AddRange(egroup.Events.Where(e => e.DateTime > DateTime.Now).ToList());
            events = events.OrderBy(e => e.DateTime).ToList();
            return PartialView(events);
        }

        //
        // GET: /Event/Details/5
        public ActionResult Details(int id)
        {
            var events = db.Events.Where(i => i.Id == id).Single();
            return View(events);
        }

        //
        // GET: /Event/Create
        public ActionResult Create(int groupId)
        {
            return View(new Event() {DateTime = DateTime.Now, GroupId = groupId});
        }

        //
        // POST: /Event/Create
        [HttpPost]
        public ActionResult Create(Event myEvent)
        {
            try
            {
                db.Events.Add(myEvent);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(myEvent);
            }
        }

        //
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            var events = db.Events.Find(id);
            DateTime time = events.DateTime;
            string location = events.Place;
            string desc = events.Description;
            string title = events.Title;
            return View(events);
            //return View();
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var events = db.Events.Single(a => a.Id == id);
                events.DateTime = events.DateTime;
                events.Description = collection.Get("Description").ToString();
                events.Place = collection.Get("Place").ToString();
                events.Title = collection.Get("Title").ToString();

                db.Entry(events).CurrentValues.SetValues(events);
                db.SaveChanges();
                return RedirectToAction("Details", "Event", new { id = events.Id });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult In(FormCollection collection)
        {
            // TODO: Add insert logic here
            bool exists = false;
            int eventId = Int32.Parse(collection.Get("EventId"));
            foreach(EventUserReply replies in db.EventUserReplies)
            {
                if(replies.AspNetUserId == User.Identity.GetUserId() && replies.EventId == eventId)
                {
                    exists = true;
                }
            }
            
            if (exists)
            {
                var reply = db.EventUserReplies.Where(i => i.AspNetUser.UserName == User.Identity.Name && i.EventId == eventId).First();
                reply.Reply = EventReply.Not_Going;

                db.Entry(reply).CurrentValues.SetValues(reply);
                db.SaveChanges();
            }
            else
            {
                EventUserReply reply = new EventUserReply();
                reply.AspNetUserId = User.Identity.GetUserId();
                reply.EventId = Int32.Parse(collection.Get("EventId"));
                reply.Reply = EventReply.Going;

                db.EventUserReplies.Add(reply);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Event", new { id = eventId });
        }

        public ActionResult Out(FormCollection collection)
        {
            // TODO: Add insert logic here
            int eventId = Int32.Parse(collection.Get("EventId"));
            var reply = db.EventUserReplies.Where(i => i.AspNetUser.UserName == User.Identity.Name && i.EventId == eventId).First();
            reply.Reply = EventReply.Not_Going;

            db.Entry(reply).CurrentValues.SetValues(reply);
            db.SaveChanges();

            return RedirectToAction("Details", "Event", new { id = reply.EventId });
        }
    }
}
