using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDoctor.Controllers
{
    public class AdminContactController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: AdminContact
       
        public ActionResult Index(int? DeleteId, int? page)
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
                list = _uw.db.Contacts
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
            
              
            return View(list);
        }


    }
}