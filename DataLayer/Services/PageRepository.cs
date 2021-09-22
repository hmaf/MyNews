    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {
        private MyCmsContext db;

        public PageRepository(MyCmsContext context)
        {
            this.db=context;
        }
        public IEnumerable<Page> GetAllPage()
        {
           return db.Pages;
        }

        public Page GetPageById(int pageId)
        {
            return db.Pages.Find(pageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool UpdatePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeletePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePage(int pageId)
        {
            try
            {
                var page = GetPageById(pageId);
                DeletePage(page);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

      
        public void save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Page> TopNews(int take = 4)
        {
            return db.Pages.OrderByDescending(v => v.Visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return db.Pages.Where(p => p.ShowInSlider == true);
        }

    

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return db.Pages.OrderByDescending(c => c.CreateDate).Take(take);
        }

        public IEnumerable<Page> ShowNewByGroupId(int groupId)
        {
            return db.Pages.Where(s => s.GroupID == groupId);
        }

        public IEnumerable<Page> SearchPages(string id)
        {
            return db.Pages.Where(s => s.Title.Contains(id) || s.ShortDescription.Contains(id) || s.Text.Contains(id) || s.Tags.Contains(id)).Distinct();
        }
    }
}
