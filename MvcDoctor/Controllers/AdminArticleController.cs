using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    [Authorize(Roles = "Administrator")]
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
            ViewBag.a = "Kategori Oluştur";
            return View();
          

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
         
        [HttpGet]
        public ActionResult UpdateCategory(int Id)
        {


            Category c = new Category();
            c = _uw.db.Categories.Find(Id);
             
            return View(c);
        }
        [HttpPost]
        public ActionResult UpdateCategory(int Id,HttpPostedFileBase Image, Category category)
        {
          
            Category c = new Category();
            c = _uw.db.Categories.Find(Id);

            c.Name = category.Name;
            var path = Server.MapPath("/");
            var ct = path + c.ImageUrl;
            System.IO.File.Delete(ct);

            string klasor = Server.MapPath("/Uploads/Category/");
            Image.SaveAs(klasor + Image.FileName);          
            c.ImageUrl = "/Uploads/Category/" + Image.FileName;      
            _uw.Categories.Update(c);
            _uw.Complete();
            return View(c);
        }
        public ActionResult Article()
        {
            return View(_uw.db.Writes.ToList());
        }


        [HttpGet]
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateArticle(string editor,string title,int CategoryId)
        {
            TempData["Category"] = _uw.Categories  // ekstradan cont cont veri de aktarır ama bir kereye mahsusyr bu özelliği
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });


            Write w = new Write();
            w.CategoryId = CategoryId;
            w.Content = editor;
            w.Title = title;
            w.ReadingCount = 0;
            _uw.Writes.Add(w);
            _uw.Complete();
             
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

        public ActionResult DeleteArticle(int Id)
        {
            Write s = _uw.db.Writes.Find(Id);
            var path = Server.MapPath("/");//klasörlerdende silmek için gerekli
             
            _uw.db.Writes.Remove(s);
            _uw.Complete();
            return RedirectToAction("Article");
        }

        [HttpGet]
        public ActionResult UpdateArticle(int Id)
        {
            TempData["Category"] = _uw.Categories  // ekstradan cont cont veri de aktarır ama bir kereye mahsusyr bu özelliği
               .GetAll()
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });

        Write w=_uw.db.Writes.Find(Id);
            return View(w);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateArticle(Write w)
        {
            TempData["Category"] = _uw.Categories  // ekstradan cont cont veri de aktarır ama bir kereye mahsusyr bu özelliği
               .GetAll()
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });

            _uw.Writes.Update(w);
            _uw.Complete();
            //Write wrt = _uw.db.Writes.Find()
            //wrt.Category = w.Category;
            //wrt.Content = w.Content;
            //wrt.Id

            return View();
        }

    }
}