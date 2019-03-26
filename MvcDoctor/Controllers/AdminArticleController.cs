using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class AdminArticleController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult Category()
        {
            return View(_uw.db.Categories.ToList());
        }
        // GET: AdminArticle
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
            ViewBag.a = "Kategori Oluştur";

        }
        [HttpPost]
        public ActionResult CreateCategory(string Category, HttpPostedFileBase Image)
        {
            ViewBag.a = "Kategori Oluştur";
            Category cr = new Category();
            string klasor = Server.MapPath("/Uploads/Category/");
            Image.SaveAs(klasor + Image.FileName);           
            cr.Name = Category.ToUpper();
            cr.ImageUrl= "/Uploads/Category/" + Image.FileName;
            if (_uw.db.Categories.Any(x=>x.Name==Category.ToUpper()))
            {
                ViewBag.a = "Böyle bir kayıt bulunmaktadır.";
            }
            else
            {
                _uw.Categories.Add(cr);
                _uw.Complete();
                ViewBag.a = "Kayıt Başarılı";
            }
           

            return View();

        }

        public ActionResult CreateArticle()
        {
            TempData["Category"] = _uw.Categories  // ekstradan cont cont veri de aktarır ama bir kereye mahsusyr bu özelliği
                 .GetAll()
                 .Select(x => new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.Id.ToString()
                 });
            return View();
        }
        public ActionResult DeleteCategory(int Id)
        {
            Category s = _uw.db.Categories.Find(Id);
            var path = Server.MapPath("/");//klasörlerdende silmek için gerekli
           
            var sm = path + s.ImageUrl;
            System.IO.File.Delete(sm);
           
            _uw.db.Categories.Remove(s);
            _uw.Complete();
            return RedirectToAction("Category");
        }
    }
}