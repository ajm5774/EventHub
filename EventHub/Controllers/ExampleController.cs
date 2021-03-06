﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventHub.Models;

namespace EventHub.Controllers
{
    public class ExampleController : Controller
    {
        //Entities extends DbContext
        private Entities db = new Entities();

        //
        // GET: /Example/
        public ActionResult Index()
        {
            //Get the rows from the Events table
            List<Event> events = db.Events.ToList();
            return View();
        }

        //
        // GET: /Example/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Example/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Example/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Example/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Example/Edit/5
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
        // GET: /Example/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Example/Delete/5
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
