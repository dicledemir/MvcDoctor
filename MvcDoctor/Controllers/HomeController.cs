using BLL;
using DAL;
using Entity;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        DoctorContext db = new DoctorContext();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _Header()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel info)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Email = info.Email;
                contact.NameSurname = info.NameSurname;
                contact.Title = info.Title;
                contact.Message = info.Message;
                db.Contacts.Add(contact);
                db.SaveChanges();
                ViewBag.Message = "Mesajınız İletilmiştir";
            }
            else
            {
                ViewBag.Message = "Bir Hata oluştu Tekrar Deneyiniz";
            }
            //ViewBag.Message = "Your contact page.";
            
             
            return View();
        }
    }
}