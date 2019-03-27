using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
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

        public ActionResult Index(int id, int? page)
        {
            ViewBag.Write = _uw.db.Writes.Where(x => x.CategoryId == id);
            var a = _uw.db.Categories.Find(id);
            ViewBag.Name = a.Name;
            ViewBag.Comments = _uw.WriteComments.GetAll().Take(5);
            ViewBag.Writes = _uw.Writes.GetAll().Take(5);
            ViewBag.kandir = ".jpg";

            List<Write> list = _uw.db.Writes.Where(x => x.CategoryId == id).ToList();
            
            if (page.HasValue)
            {
                int b = (page.Value - 1) * 3;
                list = _uw.db.Writes
                    .OrderBy(x => x.Id)
                    .Skip(b)
                    .Take(3)
                    .ToList();
            }
            else if (list.Count > 3)
            {
                list = _uw.db.Writes.Where(x => x.CategoryId == id).Take(3).ToList();
            }
            else
            {
                list = _uw.db.Writes.Where(x => x.CategoryId == id).ToList();
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

        [HttpGet]
        public ActionResult ArticleDetails(int id)
        {

            string uid = User.Identity.GetUserId();
            ViewBag.write = _uw.db.Writes.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.ıd = User.Identity.GetUserId();
            Person person = _uw.db.Users.Find(uid);
            if (person.HasPhoto)
            {
                ViewBag.Photo = "/Uploads/Person/";
                ViewBag.jpg = ".jpg";
            }

            ViewBag.Comment = _uw.db.WriteComments.Where(x => x.WriteId == id).ToList();
            Write write = _uw.db.Writes.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.WriteId = id;
            return View(write);
        }
        [HttpPost]
        public ActionResult ArticleDetails(int id, string yorum)
        {
    
            string uid = User.Identity.GetUserId();
            WriteComment wc = new WriteComment();
            wc.PersonId = uid;
            wc.Content = yorum;
             wc.WriteId = id;
            Person person = _uw.db.Users.Find(uid);
            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Person/" + uid + ".jpg";
            ViewBag.ıd = User.Identity.GetUserId();
            //Person person = _uw.db.Users.Find(uid);
          
            ViewBag.Comment = _uw.db.WriteComments.Where(x => x.WriteId == id).ToList();
            Write write = _uw.db.Writes.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.write= _uw.db.Writes.Where(x => x.Id == id).FirstOrDefault();
            _uw.WriteComments.Add(wc);
             _uw.Complete();

            return RedirectToAction("ArticleDetails");

           
        }

        public bool DeleteComment(int? deleteid)
        {
            //WriteComment delete = _uw.db.WriteComments.FirstOrDefault(x=>x.Id==deleteid);
            if(deleteid.HasValue)
            {
                _uw.WriteComments.Delete(deleteid.Value);
                _uw.Complete();
                return true;
            }
            
            else
                return false;

         }
    }
}