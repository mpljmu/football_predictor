using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace FootballPredictor.Controllers.Security
{
    public class LogInController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Log in";

            return View("~/Views/LogIn/LogIn.cshtml");
        }
    }
}
