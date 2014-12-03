using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EventHub.Models.ViewModels.User;

namespace EventHub.Controllers
{
    public class UserController : Controller
    {
        Entities db = new Entities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET:  User/Details/5
        public ActionResult Details(string id)
        {
            var user = db.AspNetUsers.Where(u => u.Id == id).Single();
            UserDetailsViewModel userVM = new UserDetailsViewModel();
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.UserName = user.UserName;
            userVM.SchoolName = user.School.Name;
            userVM.UserId = id;
            userVM.PicturePath = user.PicturePath;

            return View(userVM);
        }


    }
}