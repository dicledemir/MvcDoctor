using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class AppointmenStatusController : Controller
    {
        // GET: AppointmenStatus
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index()
        {
            //Doktor RandevuDurum tablosunda status false olan tüm kayıtları çeker.
            var appointmentList = _uw.AppointmentStatuss.Search(x => x.Status == false).ToList();
          
            return View();
        }
        public ActionResult AppointmentAccept(int appointmentId)
        {
            var appointmentstatus = _uw.AppointmentStatuss.Search(x => x.AppointmentID == appointmentId).FirstOrDefault();
            appointmentstatus.Status = true;
            var appointment = _uw.Appointments.Search(x => x.Id == appointmentId).FirstOrDefault();
            appointment.AppointmentStatus = 1;
            // SaveChanges()
            return View();
        }

        public ActionResult AppointmentDecline(int appointmentId)
        {
            var appointmentstatus = _uw.AppointmentStatuss.Search(x => x.AppointmentID == appointmentId).FirstOrDefault();
            appointmentstatus.Status = true;
            var appointment = _uw.Appointments.Search(x => x.Id == appointmentId).FirstOrDefault();
            appointment.AppointmentStatus = 2;
            // SaveChanges()
            return View();
        }

    }
}