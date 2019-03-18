using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class AdminAppointmentController : Controller
    {
        // GET: AdminAppointment
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index()
        {
            var appointmentList = _uw.Appointments.Search(x => x.Status ==Status.Waiting).ToList();
            
            return View(appointmentList);
        }
    }
}