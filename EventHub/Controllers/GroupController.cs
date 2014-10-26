using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventHub.Controllers
{
    public class GroupController : Controller
    {
        UserManager<AspNetUser> userManager;
        ApplicationDbContext authDb = new ApplicationDbContext();
        Entities db = new Entities();
        public GroupController()
        {
            userManager = new UserManager<AspNetUser>(new UserStore<AspNetUser>(authDb));
        }

        // GET:  Group/Details/5
        public ActionResult Details()
        {
            return View();
        }

        [Authorize]
        public PartialViewResult GroupSuggestions()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var school = db.Schools.Where(s => s.Id == user.SchoolId).Single();
            return PartialView(new GroupSuggestionsViewModel() {Groups = school.Groups.ToList(), School = school });
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var user = userManager.FindById(User.Identity.GetUserId());
                var school = db.Schools.Where(s => s.Id == user.SchoolId).Single();
                var group = new Group();
                group.Name = collection.GetValue("name").ToString();
                group.Description = collection.GetValue("decription").ToString();
                group.SchoolId = school.Id;
                //picture path info?
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var user = userManager.FindById(User.Identity.GetUserId());
                var group = db.Groups.Find(id);
                group.Name = collection.GetValue("name").ToString();
                group.Description = collection.GetValue("decription").ToString();
                //picture path info?
                //db.Groups.AddOrUpdate(group);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var group = db.Groups.Find(id);
                if (group != null)
                {
                    db.Groups.Remove(group);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
