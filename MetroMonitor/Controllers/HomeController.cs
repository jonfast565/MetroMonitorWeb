﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroMonitor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "MetroMonitor";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
    }
}
