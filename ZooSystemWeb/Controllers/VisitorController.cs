﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZooSystemWeb.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        public ActionResult VisitZoo()
        {
            return View();
        }
    }
}