using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult ChangePass()
        {
            return View();
        }
    }
}