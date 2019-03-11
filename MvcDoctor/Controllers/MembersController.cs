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
                    person.HasPhoto = false;
                    manager.Update(person);
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

    }
}