using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        UnitOfWork _uw = new UnitOfWork();
      
        public ActionResult Login(string error)
        { 
            ViewBag.error = error;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
           
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
                    return Json(new { success = true,});//giriş 

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
                else
                {
                    ViewBag.a = "Hata oluştu Tekrar deneyiniz.";
                    return View();
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
            ViewBag.AppointmentAccept = _uw.Appointments.Search(x => x.Status == Status.Denied).ToList();
            ViewBag.AppointmentDenied = _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            ViewBag.AppointmentWaiting = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();
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
            ViewBag.AppointmentAccept = _uw.Appointments.Search(x => x.Status == Status.Denied).ToList();
            ViewBag.AppointmentDenied = _uw.Appointments.Search(x => x.Status == Status.Accepted).ToList();
            ViewBag.AppointmentWaiting = _uw.Appointments.Search(x => x.Status == Status.Waiting).ToList();

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
         public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
         public ActionResult RecoverPassword(RecoverPasswordViewModel model)
        {

            UserStore<Person> store = new UserStore<Person>(_uw.db);
            UserManager<Person> userManager = new UserManager<Person>(store);
            var user = userManager.FindByEmail(model.Email);
  
            if (user != null)
            {
                var newPass = CreatePassword();
                store.SetPasswordHashAsync(user, userManager.PasswordHasher.HashPassword(newPass));
                _uw.Complete();
                
                var body = $"Merhaba <b>{user.UserName}</br> Hesabınızın Parolası sıfırlanmıştır.  Yeni Parolanız:{newPass}<p>Yukarıdaki parolayı kullanarak sisteme giriş yapabilirsiniz";
                
                WebMail.SmtpServer= "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "MmvcDoctor@gmail.com";
                WebMail.Password = "Aa123456!";
                WebMail.SmtpPort = 587;
                WebMail.Send(
                    model.Email,
                    "Şifre sıfırlama",
                    body
                    );
                ViewBag.b = "Email adresinizize yeni şifreniz gönderilmiştir.";
                return View();



            }
            else
            {
                ViewBag.a = "Email adresine kayıtlı üyelik bulunamadı";
                return View();
            }
                return View();
        }



        public string CreatePassword()
        {
            Random number = new Random();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int ascii = number.Next(32, 127);
                char character = Convert.ToChar(ascii);
                   sb.Append(character);
            }
            return (sb.ToString());
        }
    }
}