using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;

namespace EventHub.Controllers
{
    public class EventController : Controller
    {
        Entities db = new Entities();

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

        //
        // GET: /Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Event/Create
        public ActionResult Create()
        {
            return View(new Event() {DateTime = DateTime.Now});
        }

        //
        // POST: /Event/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
    }
}
