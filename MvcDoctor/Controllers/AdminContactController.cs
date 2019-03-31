using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminContactController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: AdminContact
        public ActionResult Index(int? DeleteId, int? page, string Search)
        {
            List<Contact> list = _uw.Contacts.GetAll();
            ViewBag.page = page;           
            if (DeleteId.HasValue)
            {
                _uw.Contacts.Delete(DeleteId.Value);
                _uw.Complete();
                return RedirectToAction("Index");                 
            }
            if (page.HasValue)
            {
                int b = (page.Value - 1) * 4;
                list =list
                    .OrderBy(x => x.Id)
                    .Skip(b)
                    .Take(4)
                    .ToList();
            }
            else if (list.Count > 4)
            {
                list = _uw.db.Contacts.Take(4).ToList();
            }
            else
            {
                list = _uw.Contacts.GetAll();
            }
            float PageCount = _uw.db.Contacts.Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;           
            if (!String.IsNullOrEmpty(Search))
            { 
                list = _uw.db.Contacts.Where(a => a.NameSurname.Contains(Search)).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult MyAccount()
        {
            MyAccountViewModel myaccountmodel = new MyAccountViewModel();
            string uid = User.Identity.GetUserId();
            Person person = _uw.db.Users.Find(uid);
            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + uid + ".jpg";

            myaccountmodel.Email = person.Email;
            myaccountmodel.PhoneNumber = person.PhoneNumber;           
            return View(myaccountmodel);
        }
        [HttpPost]
        public ActionResult MyAccount(MyAccountViewModel info, HttpPostedFileBase Image)
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
            UserManager<Person> manager = new UserManager<Person>(store);
            string uid = User.Identity.GetUserId();
            Person person = manager.FindById(uid);

            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + uid + ".jpg";
            person.Email = info.Email;
            person.PhoneNumber = info.PhoneNumber;
            

            if (Image != null)
            {
                string path = Server.MapPath("/Uploads/Person/");
                string old = path + person.Id + "jpg";
                if (System.IO.File.Exists(old))
                    System.IO.File.Delete(old);
                string _new = path + person.Id + ".jpg";
                Image.SaveAs(_new);
                person.HasPhoto = true;
            }
            manager.Update(person);

            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + uid + ".jpg";
            return View(info);
        }



        [HttpGet]
        public ActionResult Membership(int? page, string Search)
        {
            var list = _uw.db.Users.ToList();
            ViewBag.kandir = ".jpg";
            if (page.HasValue)
            {
                if (Search == null)
                {

                    int b = (page.Value - 1) * 4;
                    list = list
                        .Skip(b)
                        .Take(4)
                        .ToList();
                }
                else
                {
                    int b = (page.Value - 1) * 4;
                    list = _uw.db.Users.Where(a => a.UserName.Contains(Search))
                        .Skip(b)
                        .Take(4)
                        .ToList();
                }
            }
            else if(list.Count > 4)
            {
                if (Search == null)
                    list = list.Take(4).ToList();
                else
                    list = _uw.db.Users.Where(a => a.UserName.Contains(Search)).Take(4).ToList();
            }
            else
            {
                if (Search == null)
                    list.ToList();
                else
                    list = _uw.db.Users.Where(a => a.UserName.Contains(Search)).ToList();
            }

            float PageCount = _uw.db.Contacts.Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;
             
            return View(list);
        }
        [HttpPost]
        public ActionResult Membership(int? page, string Search,string d)
        {

            var list = _uw.db.Users.ToList();
            ViewBag.kandir = ".jpg";

            if (!String.IsNullOrEmpty(Search))
            {
                list = _uw.db.Users.Where(a => a.UserName.Contains(Search)).ToList();
            }

            if (page.HasValue)
            {
                if (Search == null) {

                    int b = (page.Value - 1) * 4;
                    list = list
                        .Skip(b)
                        .Take(4)
                        .ToList();
                    }
                else {
                    int b = (page.Value - 1) * 4;
                    list =_uw.db.Users.Where(a => a.UserName.Contains(Search))
                        .Skip(b)
                        .Take(4)
                        .ToList();
                }
            }
            else if (list.Count > 4)
            {

                if (Search == null)
                    list = list.Take(4).ToList();
                else
                    list = _uw.db.Users.Where(a => a.UserName.Contains(Search)).Take(4).ToList();
            }
            else
            {
                if (Search == null)
                    list.ToList();
                else
                    list = _uw.db.Users.Where(a => a.UserName.Contains(Search)).ToList();
            }

            float PageCount = _uw.db.Contacts.Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;

           
            return View(list);
           
        }

    }
}