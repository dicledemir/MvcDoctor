using BLL;
using Entity;
using Entity.ViewModels;
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
        
        public ActionResult Index(int? id)
        {

            ViewBag.Person = _uw.Appointments.Search(x => x.Id == id).FirstOrDefault();



            ViewBag.appointmentList = _uw.Appointments.Search(x => x.Status ==Status.Waiting).ToList();
           var  appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            ViewBag.kandir =".jpg";
 
            return View(appointmentList);
        }
        [HttpPost]
        public ActionResult Index(int id)
        {



            ViewBag.appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            var appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            ViewBag.kandir = ".jpg";
            return View(appointmentList);
        }

        public JsonResult Update(string id,int sonuc,int RandevuId)
        {
            var appointmentstatus = _uw.Appointments.Search(x => x.PersonId == id&x.Id== RandevuId).FirstOrDefault();
            //var a=   _uw.Appointments.Search(id).FirstOrDefault();
            if (sonuc == 2)
            {
               
                appointmentstatus.Status = Status.Accepted;
                _uw.Complete();

            }
            else if (sonuc == 1)
            {
                appointmentstatus.Status = Status.Denied;
                _uw.Complete();

            }



            var result=sonuc;
            
            return Json(result);
        }

        public ActionResult PersonDeatils(int id)
        {

            Appointment Appointmentlist = new Appointment();
           
            Person person = _uw.db.Users.Find(id);
            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + id + ".jpg";

            //Appointmentlist = _uw.Appointments.Search(x=>x.Id==)
            //myaccountmodel.PhoneNumber = person.PhoneNumber;


            //myaccountmodel.AppointmentAccept = _uw.Appointments.Search(x => x.Status == Status.Denied).ToList();
            //myaccountmodel.AppointmentDenied = _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            //myaccountmodel.AppointmentWaiting = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();

            return View();
        }
    }
}