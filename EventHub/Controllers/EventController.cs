using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

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
            List<EventViewModel> events = new List<EventViewModel>();

            var id = User.Identity.GetUserId();
            var eventGroups = from gs in db.GroupSubscriptions.Where(s => s.AspNetUserId == id).ToList()
                             join g in db.Groups on gs.GroupId equals g.Id
                             select new
                             {
                                 Events = g.Events
                             };
            var eventGroupsList = eventGroups.ToList();

            bool receiveNotifications;
            UserEventNotification userEventNotification;
            foreach(var eventGroup in eventGroupsList)
            {
                foreach(var anEvent in eventGroup.Events.Where(e => e.DateTime > DateTime.Now).ToList())
                {
                    userEventNotification = anEvent.UserEventNotifications.Where( uen => uen.AspNetUsersId == id).FirstOrDefault();
                    receiveNotifications = userEventNotification == null? true : userEventNotification.AllowNotifications;
                    events.Add( new EventViewModel(){ AnEvent = anEvent, ReceiveNotifications = receiveNotifications});
                }
            }
            events = events.OrderBy(e => e.AnEvent.DateTime).ToList();
            return PartialView(events);
        }

        public PartialViewResult GroupEventFeed(int id)
        {
            var userid = User.Identity.GetUserId();
            List<EventViewModel> events = new List<EventViewModel>();

            var egroup = db.Groups.Where(g => g.Id == id).Single();

            bool receiveNotifications;
            UserEventNotification userEventNotification;
            foreach (var anEvent in egroup.Events.Where(e => e.DateTime > DateTime.Now).ToList())
            {
                userEventNotification = anEvent.UserEventNotifications.Where(uen => uen.AspNetUsersId == userid).FirstOrDefault();
                receiveNotifications = userEventNotification == null? true : userEventNotification.AllowNotifications;
                events.Add( new EventViewModel(){ AnEvent = anEvent, ReceiveNotifications = receiveNotifications});
            }
            events = events.OrderBy(e => e.AnEvent.DateTime).ToList();
            return PartialView(events);
        }

        public PartialViewResult GroupEventFeedPast(int id)
        {
            var userid = User.Identity.GetUserId();
            List<EventViewModel> events = new List<EventViewModel>();

            var egroup = db.Groups.Where(g => g.Id == id).Single();
            bool receiveNotifications;
            UserEventNotification userEventNotification;
            foreach (var anEvent in egroup.Events.Where(e => e.DateTime <= DateTime.Now).ToList())
            {
                userEventNotification = anEvent.UserEventNotifications.Where(uen => uen.AspNetUsersId == userid).FirstOrDefault();
                receiveNotifications = userEventNotification == null ? true : userEventNotification.AllowNotifications;
                events.Add(new EventViewModel() { AnEvent = anEvent, ReceiveNotifications = receiveNotifications });
            }
            events = events.OrderByDescending(e => e.AnEvent.DateTime).ToList();
            return PartialView(events);
        }

        //
        // GET: /Event/Details/5
        public ActionResult Details(int id)
        {
            var anEvent = db.Events.Where(i => i.Id == id).Single();
            var userId = User.Identity.GetUserId();

            var userEventNotification = anEvent.UserEventNotifications.Where(uen => uen.AspNetUsersId == userId).FirstOrDefault();
            var receiveNotifications = userEventNotification == null ? true : userEventNotification.AllowNotifications;
            var eventViewModel = new EventViewModel() { AnEvent = anEvent, ReceiveNotifications = receiveNotifications };

            return View(eventViewModel);
        }

        public ActionResult ToggleEventNotifications(int id)
        {
            //var id = Int32.Parse(collection.Get("EventId"));
            var userId = User.Identity.GetUserId();

            var userEventNotification = db.UserEventNotifications
                .Where(uen => id == uen.EventsId && uen.AspNetUsersId == userId)
                .FirstOrDefault();

            if (userEventNotification == null)
            {
                userEventNotification = new UserEventNotification()
                {
                    AspNetUsersId = userId,
                    EventsId = id,
                    AllowNotifications = false
                };
                db.UserEventNotifications.Add(userEventNotification);
            }
            else
                userEventNotification.AllowNotifications = !userEventNotification.AllowNotifications;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
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

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult UploadPictures(UploadViewModel uploadModel)
        {
            foreach (var file in uploadModel.Files)
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // store the file inside ~/App_Data/uploads folder
                    var modelPath = Path.Combine("\\Content\\Images\\Uploads", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    var serverPath = Path.Combine(Server.MapPath(modelPath));
                    if (!Directory.Exists(serverPath))
                        Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                    file.SaveAs(serverPath);

                    EventPicture ep = new EventPicture() { EventId = uploadModel.id, PicturePath = modelPath };
                    db.EventPictures.Add(ep);
                }
            }
            db.SaveChanges();
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

    }
}
