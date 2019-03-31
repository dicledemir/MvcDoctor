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
        //[Authorize(Roles = "Administrator")]
        // GET: AdminAppointment
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index(int? id,  int? page)
        { 
            ViewBag.Person = _uw.Appointments.Search(x => x.Id == id).FirstOrDefault();             
            ViewBag.appointmentList = _uw.Appointments.Search(x => x.Status ==Status.Waiting).ToList();
           var  appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            ViewBag.kandir =".jpg";
            List<Appointment> list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).ToList();

            if (page.HasValue)
            {
                int b = (page.Value - 1) * 4;
                 list = list                  
                    .Skip(b)
                    .Take(4)
                    .ToList();
            }
            else if (list.Count > 4)
            {
                list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).Take(4).ToList();
            }
            else
            {
                list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).ToList();
            }
             
            float PageCount = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;
             
            return View(list);
             
        }
        [HttpPost]
        public ActionResult Index(int? id, string Search, int? page)
        { 
            ViewBag.appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            var appointmentList = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
            ViewBag.kandir = ".jpg";

            List<Appointment> list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).ToList();

            if (!String.IsNullOrEmpty(Search))
            {

                list = appointmentList.Where(a => a.person.UserName.Contains(Search)).ToList();
               
            }
             

            if (page.HasValue)
            {
                if (Search == null) {
                    int b = (page.Value - 1) * 4;
                list = list
                      .Skip(b)
                      .Take(4)
                      .ToList(); }
                else
                {
                    int b = (page.Value - 1) * 4;
                    list = appointmentList.Where(a => a.person.UserName.Contains(Search))
                          .Skip(b)
                          .Take(4)
                          .ToList();
                }
            }
            else if (list.Count > 4)
            {
                if(  Search==null)
                list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting  ).Take(4).ToList();
                else
                    list = appointmentList.Where(a => a.person.UserName.Contains(Search)).Take(4).ToList();

            }
            else
            {
                if (Search == null)
                    list = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).ToList();
                else
                    list = appointmentList.Where(a => a.person.UserName.Contains(Search)).ToList();

            }

            float PageCount = _uw.db.Appointments.Where(x => x.Status == Status.Waiting).Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;
             
            return View(list);
        }

        public JsonResult Update(string id,int sonuc,int RandevuId)
        {
            var appointmentstatus = _uw.Appointments.Search(x => x.PersonId == id&x.Id== RandevuId).FirstOrDefault();
            //var a=   _uw.Appointments.Search(id).FirstOrDefault();
            var data = false;
            if (sonuc == 2)
            { 
                appointmentstatus.Status = Status.Accepted;
                _uw.Complete();
                data = true;

            }
            else if (sonuc == 1)
            {
                appointmentstatus.Status = Status.Denied;
                _uw.Complete();
                data = true;

            }
             
            return Json(data);
        }

        public ActionResult PersonDetails(string id)
        { 
              Person person = _uw.db.Users.Find(id);
             if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + id + ".jpg";
              
            ViewBag.AppointmentAccept = _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            ViewBag.AppointmentDenied = _uw.Appointments.Search(x => x.Status == Status.Denied).ToList();
            ViewBag.AppointmentWaiting = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();


            return View(person);

            
        }
    }
}