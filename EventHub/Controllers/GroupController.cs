﻿using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult UploadPicture(UploadViewModel uploadModel)
        {
            var file = uploadModel.File;
            var groupid = uploadModel.id;
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // store the file inside ~/App_Data/uploads folder
                var modelPath = Path.Combine("\\Content\\Images\\Uploads", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                var serverPath = Path.Combine(Server.MapPath(modelPath));
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                file.SaveAs(serverPath);

                Group group = db.Groups.Where(g => g.Id == groupid).Single();
                group.PicturePath = modelPath;
            }

            db.SaveChanges();
            // redirect back to the index action to show the form once again
            return RedirectToAction("Details", "Group", new { id = groupid});
        }

        public PartialViewResult GetMembers(int id)
        {
            List<UserViewModel> attendees = new List<UserViewModel>();
            string name;
            foreach (var sub in db.GroupSubscriptions.Where(g => g.GroupId == id))
            {
                name = sub.AspNetUser.FirstName + " " + sub.AspNetUser.LastName;
                attendees.Add(new UserViewModel() { Name = name, picturePath = sub.AspNetUser.PicturePath });
            }
            return PartialView(attendees);
        }

        // GET:  Group/Details/5
        public ActionResult Details(int id)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var group = db.Groups.Where(i => i.Id == id).Single();
            var requests = db.AdminRequests.Where(r => r.GroupId == id).ToList();

            return View(new GroupDetailsViewModel() { Group = group, AdminRequests = requests, ViewingUserId = user.Id });
        }

        // GET:  Group/AdminRequests/5
        public ActionResult AdminRequests(int groupId)
        {
            var requests = db.AdminRequests.Where(r => r.GroupId == groupId).ToList();

            return View(new GroupAdminRequestsViewModel() { AdminRequests = requests, GroupId = groupId });
        }

        // GET:  Group/AdminRequest/5
        public ActionResult AdminRequest(int groupId, string userId)
        {
            var group = db.Groups.Where(i => i.Id == groupId).Single();
            var user = db.AspNetUsers.Where(u => u.Id == userId).Single();
            var adminReq = new AdminRequest();
            adminReq.AspNetUserId = user.Id;
            adminReq.GroupId = groupId;
            adminReq.Group = group;
            adminReq.Requester = user;
            db.AdminRequests.Add(adminReq);
            db.SaveChanges();

            return View();
            
        }

        [Authorize]
        public PartialViewResult MyGroups()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var groups = (from gs in db.GroupSubscriptions.Where(s => s.AspNetUserId == user.Id).ToList()
                          join g in db.Groups on gs.GroupId equals g.Id
                          select g).ToList();
            return PartialView(groups);
        }

        public PartialViewResult MyUsers(int id)
        {
            var users = (from gs in db.GroupSubscriptions.Where(gs => gs.GroupId == id).ToList()
                         join u in db.AspNetUsers on gs.AspNetUserId equals u.Id
                         select u).ToList();
            return PartialView(users);

        }

        [Authorize]
        public PartialViewResult UserGroups(string user_id)
        {
            var groups = (from gs in db.GroupSubscriptions.Where(s => s.AspNetUserId == user_id).ToList()
                          join g in db.Groups on gs.GroupId equals g.Id
                          select g).ToList();
            return PartialView(groups);
        }

        [Authorize]
        public PartialViewResult GroupSuggestions()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var school = db.Schools.Where(s => s.Id == user.SchoolId).Single();
            List<Group> Groups = new List<Group>();
            List<Group> temp = new List<Group>(school.Groups.ToList());
            int index = 0;
            //remove groups that the user is already subscribed to
            foreach(GroupSubscription gs in user.GroupSubscriptions)
            {
                bool subscribed = false;
                foreach(Group g in temp)
                {
                    if (g.Id == gs.GroupId)
                    {
                        index = temp.IndexOf(g);
                        subscribed = true;
                        break;
                    }
                }
                if (subscribed)
                {
                    temp.RemoveAt(index);
                }
            }
            //if suggested groups is still more than 10, limit it
            if(temp.Count > 10)
            {
                for(int i = 0; i < 10; i++)
                {
                    Groups.Add(temp.ElementAt(i));
                }
            }
            else
            {
                Groups = temp;
            }
            return PartialView(new GroupSuggestionsViewModel() { Groups = Groups, School = school });
        }


        [Authorize]
        public PartialViewResult GroupSearch(string searchTerm)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var school = db.Schools.Where(s => s.Id == user.SchoolId).Single();
            List<Group> Groups = new List<Group>();
            List<Group> temp = new List<Group>(school.Groups.Where(s => (s.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)));
            int index = 0;
            //remove groups that the user is already subscribed to
            foreach (GroupSubscription gs in user.GroupSubscriptions)
            {
                bool subscribed = false;
                foreach (Group g in temp)
                {
                    if (g.Id == gs.GroupId)
                    {
                        index = temp.IndexOf(g);
                        subscribed = true;
                        break;
                    }
                }
                if (subscribed)
                {
                    temp.RemoveAt(index);
                }
            }
            //if suggested groups is still more than 10, limit it
            if (temp.Count > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Groups.Add(temp.ElementAt(i));
                }
            }
            else
            {
                Groups = temp;
            }
            return PartialView("GroupSuggestions", new GroupSuggestionsViewModel() { Groups = Groups, School = school });
        }

        [Authorize]
        public ActionResult Add(FormCollection collection)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var id = Int32.Parse(collection.Get("GroupId"));
            GroupSubscription gs = new GroupSubscription() { GroupId = id, AspNetUserId = user.Id };
            db.GroupSubscriptions.Add(gs);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Remove(FormCollection collection)
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var id = Int32.Parse(collection.Get("GroupId"));
            var groupSub = db.GroupSubscriptions.Where(gs => gs.AspNetUserId == user.Id && gs.GroupId == id).First();
            db.GroupSubscriptions.Remove(groupSub);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Admin(int id, FormCollection collection)
        {
            var UId = collection.Get("Id");
            var groupSub = db.GroupSubscriptions.Where(gs => gs.AspNetUserId == UId && gs.GroupId == id).First();
            groupSub.IsAdministrator= true;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddAdmin(int groupId, FormCollection collection)
        {
            var UId = collection.Get("UserId");
            var groupSub = db.GroupSubscriptions.Where(gs => gs.AspNetUserId == UId && gs.GroupId == groupId).First();
            var adminReq = db.AdminRequests.Where(r => r.AspNetUserId == UId && r.GroupId == groupId).Single();
            //var updatedGS = groupSub;
            //updateGS.IsAdministrator = true;
            db.AdminRequests.Remove(adminReq);

            groupSub.IsAdministrator = true;
            db.GroupSubscriptions.Attach(groupSub);
            var entry = db.Entry(groupSub);
            entry.Property(e => e.IsAdministrator).IsModified = true;
            
            //var entry = db.Entry(updatedGS);
            //entry.Property(e => e.IsAdministrator).IsModified = true;
            //db.Entry(groupSub).CurrentValues.SetValues(updatedGS);
            
            db.SaveChanges();

            return RedirectToAction("Details", "Group", new { id = groupId });
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
                group.Name = collection.Get("Name").ToString();
                group.Description = collection.Get("Description").ToString();
                group.SchoolId = school.Id;
                group.PicturePath = "\\Content\\Images\\testImages\\group_default.png";
                //picture path info?
                db.Groups.Add(group);
                //Add user to group subscription
                GroupSubscription gs = new GroupSubscription() { GroupId = group.Id, AspNetUserId = user.Id, IsAdministrator = true};
                db.GroupSubscriptions.Add(gs);

                db.SaveChanges();
                return RedirectToAction("Details", "Group", new { id=group.Id, success = true });
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            var group = db.Groups.Find(id);
            string name = group.Name;
            string desc = group.Description;
            string picPath = group.PicturePath;
            return View(group);
            //return View(new EditGroupViewModel() { Name = name, Description = desc, PicturePath = picPath});
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var user = userManager.FindById(User.Identity.GetUserId());
                var group = db.Groups.Single(a => a.Id == id);
                group.Name = collection.Get("Name").ToString();
                group.Description = collection.Get("Description").ToString();

                //db.Groups.Attach(group);
                db.Entry(group).CurrentValues.SetValues(group);
                db.SaveChanges();
                return RedirectToAction("Details", "Group", new { id = group.Id });
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var group = db.Groups.Find(id);

                if (group != null)
                {
                    var userid = User.Identity.GetUserId();
                    if (group.GroupSubscriptions.Where(gs => gs.AspNetUser.Id == userid).Single() == null)
                    {
                        return RedirectToAction("Index", "Home", new { success = false });
                    }
                    
                    foreach(var gs in group.GroupSubscriptions.ToList())
                    {
                        db.GroupSubscriptions.Remove(gs);
                    }
                    db.SaveChanges();
                    foreach (var e in group.Events.ToList())
                    {
                        db.Events.Remove(e);
                    }
                    db.SaveChanges();
                    foreach (var ar in group.AdminRequests.ToList())
                    {
                        db.AdminRequests.Remove(ar);
                    }
                    db.SaveChanges();
                    group = db.Groups.Find(id);
                    db.Groups.Remove(group);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new { success = true });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Home", new { success = true });
            }
        }
    }
}