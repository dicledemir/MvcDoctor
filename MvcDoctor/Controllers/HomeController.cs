using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _Header()
        {
            return View();
        }
        public ActionResult _About()
        {
            return View();
        }
        public ActionResult _Contact()
        {
            return View();
        }
        public ActionResult _Appointment()
        {
            return View();
        }
        public ActionResult _Write()
        {
            return View();
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