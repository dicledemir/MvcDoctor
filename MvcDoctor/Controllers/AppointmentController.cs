using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class AppointmentController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: Appointment
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Appointment info)
        {
            if (User.Identity.IsAuthenticated) {
                if (ModelState.IsValid)
                {
                    string uid = User.Identity.GetUserId();
                    //Person user = _uw.db.Users.Find(uid);
                    //info.person = user;
                    info.PersonId = uid;
                    info.AppointmentStatus = 0;
                    _uw.Appointments.Add(info);
                    _uw.Complete();
                    ViewBag.A = "AA";
                    return RedirectToAction("Index");
                }
                 
            }
            else
            {

            }
             
            return View();
        }


        public ActionResult AppointmentDetail()
        {
            return View();
        }
    }
}