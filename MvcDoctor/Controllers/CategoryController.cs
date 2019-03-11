using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class CategoryController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            

            return View( );
        }
        [HttpPost]
        public ActionResult Index(string a)
        {

            return View();
        }
    }
}