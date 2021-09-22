using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Areas.ADMIN.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        private IPageRepository pageRepository;
        private IPageGroupRepository pageGroupRepository;
        private MyCmsContext db = new MyCmsContext();

        public PagesController()
        {
            pageRepository = new PageRepository(db);
            pageGroupRepository = new PageGroupRepository(db);
        }
        // GET: ADMIN/Pages
        public ActionResult Index()
        {
           
            return View(pageRepository.GetAllPage());
        }

        // GET: ADMIN/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: ADMIN/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pageGroupRepository.GetAllGroup(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: ADMIN/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                page.Visit = 0;
                DateTime now = DateTime.Now;
                page.CreateDate = now;

                if (imgUp != null)
                {
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }
                pageRepository.InsertPage(page);
                pageRepository.save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.PageGroups, "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: ADMIN/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(pageGroupRepository.GetAllGroup(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: ADMIN/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (page.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
                    }

                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }

                pageRepository.UpdatePage(page);
                pageRepository.save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.PageGroups, "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: ADMIN/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: ADMIN/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = pageRepository.GetPageById(id);
            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
            }
            pageRepository.DeletePage(page);
            pageRepository.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pageGroupRepository.Dispose();
                pageRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
