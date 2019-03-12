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
    public class ArticleController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: Category
 
        public ActionResult Index(int id,int? page)
        {


            ViewBag.Write = _uw.db.Writes.Where(x => x.CategoryId == id);

            var a = _uw.db.Categories.Find(id);

            ViewBag.Name= a.Name;


            List<Write> list = _uw.db.Writes.Where(x => x.CategoryId == id).ToList();


            if (page.HasValue)
            {
                int b = (page.Value - 1)*3;  
                list = _uw.db.Writes
                    .OrderBy(x => x.Id)
                    .Skip(b) 
                    .Take(3)
                    .ToList();
            }
            else
            {
                list = _uw.db.Writes.Take(3).ToList();
            }
            ArticleViewModel wm = new ArticleViewModel();

            float PageCount = _uw.db.Writes.Where(x => x.CategoryId == id).Count();
            int Current = page.HasValue ? page.Value : 1;
            ViewBag.Start = Current > 2 ? Current - 2 : 1;
            ViewBag.End = Current < PageCount - 2 ? Current + 2 : PageCount;
            ViewBag.PrevVisible = Current > 1;
            ViewBag.NextVisible = Current < PageCount;
            ViewBag.CurrentPage = Current;
            wm.Writes = list;
            wm.Categories = _uw.db.Categories.Find(id);           
            return View(wm);
        }
         
        public ActionResult _Article()
        {
            ViewBag.article = _uw.Categories.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View();
        }
    }
}