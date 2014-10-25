﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using EventHub.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventHub.Controllers
{
    public class HomeController : Controller
    {
        UserManager<AspNetUser> userManager;
        ApplicationDbContext db = new ApplicationDbContext();
        public HomeController()
        {
            userManager = new UserManager<AspNetUser>(new UserStore<AspNetUser>(db));
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            return View(new IndexViewModel() { PicturePath = user.PicturePath});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}