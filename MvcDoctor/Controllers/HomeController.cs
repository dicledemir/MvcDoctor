using BLL;
using DAL;
using Entity;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Slider s = new Slider();
            HomeViewModel hvm = new HomeViewModel();
            hvm.Sliders = db.Sliders.ToList();
            return View(hvm); 
        }

        [HttpGet]
        public ActionResult SliderCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderCreate(HttpPostedFileBase imagefile)
        {
            if (imagefile != null && imagefile.ContentLength != 0)
            {
                string path = Server.MapPath("~/Uploads/Sliders/");
                string largepath = path + "large/";
                string thumbpath = path + "thumb/";
              
                imagefile.SaveAs(largepath + imagefile.FileName);

                Image i = Image.FromFile(largepath + imagefile.FileName);
                Size s = new Size(200, 50);
                Image small = Helper.ResizeImage(i, s); //kçük resmi elde ettik
                small.Save(thumbpath + imagefile.FileName);//küçük resmi kaydettik
                                                           //klasörlere kaydettik şimdi veri tabanına kaydecez
                i.Dispose();//en son eklenen resm o an silinmek istediğinde hata verdiği için bu eklendi çöpe atıyor (arastr bunu)
                

                Slider slider = new Slider();
                //img src içinde göstereceğimiz için relative path kaydediyoruz
                slider.LargeImageUrl = "/Uploads/Sliders/large/" + imagefile.FileName;
                ////small kaydettik
                slider.ThumbnailUrl = "/Uploads/Sliders/thumb/" + imagefile.FileName;

                db.Sliders.Add(slider);
                db.SaveChanges();
                return View();
            }


            return View();
        }



        public ActionResult Slider()
        {

            return View(db.Sliders.ToList());
        }

        public ActionResult DeleteSlider(int Id)
        {
            Slider s = db.Sliders.Find(Id);
            var path = Server.MapPath("/");//klasörlerdende silmek için gerekli
            var lg = path + s.LargeImageUrl;
            var sm = path + s.SliderId;
            System.IO.File.Delete(lg);
            System.IO.File.Delete(sm);
            db.Sliders.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Slider");
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
                return View();
            
            }
            else
            {
                ViewBag.Message = "Bir Hata oluştu Tekrar Deneyiniz";
                return View();
            }
            //ViewBag.Message = "Your contact page.";

            return View();

        }
    }
}