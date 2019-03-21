using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
        UnitOfWork _uw = new UnitOfWork();
       
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult _Login(LoginViewModel info)
        {
            //1 Signin manegare  ulaş
            ApplicationSignInManager sigInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            //2. Giriş yapmayı dene(result dene.)

            SignInStatus result = sigInManager.PasswordSignIn(info.Email, info.Password, true, false);
            //3. ilgili Durumlara göre sonucu döndür


            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new { success = true, url = "/Index/Members" });//giriş yaptıysa ana sayfaya gitsin
                                                                       //case SignInStatus.LockedOut://bu kişiyi kitlediysek

                //   break;
                //case SignInStatus.RequiresVerification:
                //    //tüm hatalar için view oluşturursun onun view yollarsın( mail ile dogrulama)
                //    break;

                case SignInStatus.Failure:
                    return Json(new { success = false });

            }
            return Json(new { success = false });
        }
  
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string oldp, string newp)
        {
            UserStore<Person> store = new UserStore<Person>(_uw.db);
            UserManager<Person> manager = new UserManager<Person>(store);
            string uid = User.Identity.GetUserId();
            Person p = manager.FindById(uid);
            bool isCorrect = manager.CheckPassword(p, oldp);
            if (isCorrect)
            {
                IdentityResult r = manager.ChangePassword(uid, oldp, newp);
                if (r.Succeeded)
                    ViewBag.Success = true;
                else
                    ViewBag.Errors = r.Errors;
            }
            else
            {
                ViewBag.WrongPassword = true;
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
       [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(Person person,string Pass,string PassR)
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
            UserManager<Person> manager = new UserManager<Person>(store);

            if (Pass == PassR)
            {
                var result = manager.Create(person, Pass);

                if (result.Succeeded)
                {
                    
                    manager.Update(person);
                    person.HasPhoto = false;
                    return RedirectToAction("Index", "Home");
                }

            }

            return View();
        }

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Index", "Home");
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
            
            myaccountmodel.AppointmentAccept = _uw.Appointments.Search(x => x.Status ==Status.Denied).ToList();
            myaccountmodel.AppointmentDenied= _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            myaccountmodel.AppointmentWaiting = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
    

            return View(myaccountmodel);
        }
        [HttpPost]
        public ActionResult MyAccount(MyAccountViewModel info , HttpPostedFileBase Image  )
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());

            UserManager<Person> manager = new UserManager<Person>(store);
            string uid = User.Identity.GetUserId();
            Person person = manager.FindById(uid);

            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + uid + ".jpg";

            person.Email = info.Email;
            person.PhoneNumber = info.PhoneNumber;
            info.AppointmentAccept = _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            info.AppointmentDenied = _uw.Appointments.Search(x => x.Status ==Status.Denied).ToList();

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

    }
}