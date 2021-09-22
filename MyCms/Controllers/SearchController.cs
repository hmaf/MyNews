using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Controllers
{
    public class SearchController : Controller
    {
        private IPageRepository PageRepository;
        MyCmsContext db = new MyCmsContext();

        public SearchController()
        {
            PageRepository = new PageRepository(db);
        }
        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.name = q;
            return View(PageRepository.SearchPages(q));
        }
    }
}