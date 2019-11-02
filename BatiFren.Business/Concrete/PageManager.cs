using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class PageManager : IPageService
    {
        private IPageDal _pageDal;
        public PageManager(IPageDal pageDal)
        {
            _pageDal = pageDal;
        }
        public void Add(Page page)
        {
            _pageDal.Add(page);
        }

        public void Delete(Page page)
        {
            _pageDal.Delete(page);
        }

        public Page FindByUrl(string url)
        {
            return _pageDal.GetLazyFirstOrDefault(x => x.Url == url, x => x.PageDetails);
        }

        public Page GetByPageID(int id)
        {
            return _pageDal.FindFirst(x=>x.PageID==id);
        }

        public List<Page> GetList()
        {
            return _pageDal.GetList();
        }

        public void Update(Page page)
        {
            _pageDal.Update(page);
        }
    }
}
