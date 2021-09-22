using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Controllers
{
    public class NewsController : Controller
    {
        MyCmsContext db = new MyCmsContext();

        private IPageGroupRepository pageGroupRepository;
        private IPageRepository pageRepository;
        private IPageCommentRepository pageCommentRepository;
        public NewsController()
        {
            pageGroupRepository = new PageGroupRepository(db);
            pageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }

        // GET: News
        public ActionResult ShowGroup()
        {
            return PartialView(pageGroupRepository.GetGroupsForView());
        }

        public ActionResult ShowGroupInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroup());
        }

        public ActionResult TopNews()
        {
            return PartialView(pageRepository.TopNews());
        }

        public ActionResult LastNews()
        {
            return PartialView(pageRepository.LastNews());
        }
        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(pageRepository.GetAllPage());
        }

        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id,string title)
        {
            ViewBag.name = title;
            return View(pageRepository.ShowNewByGroupId(id));
        }

        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = pageRepository.GetPageById(id);

            if (news==null)
            {
               return HttpNotFound();
            }

            news.Visit += 1;
            pageRepository.UpdatePage(news);
            pageRepository.save();
            return View(news);
        }

        public ActionResult AddComment(int id,string name,string email,string comment)
        {
            PageComment addcomment = new PageComment()
            {
                CreateDate = DateTime.Now,
                PageID = id,                             
                Comment = comment,
                Email = email,
                Name = name
            };
            pageCommentRepository.AddComment(addcomment);

            return PartialView("ShowComments",pageCommentRepository.GetCommentByNewsId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByNewsId(id));
        }
    }

}