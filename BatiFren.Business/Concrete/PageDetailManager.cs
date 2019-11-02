using System.Collections.Generic;
using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;

namespace BatiFren.Business.Concrete
{
    public class PageDetailManager : IPageDetailService
    {
        private IPageDetailDal _pageDetailDal;
        public PageDetailManager(IPageDetailDal pageDetailDal)
        {
            _pageDetailDal = pageDetailDal;
        }

        public void Add(PageDetail pageDetail)
        {
            _pageDetailDal.Add(pageDetail);
        }

        public void Delete(PageDetail pageDetail)
        {
            _pageDetailDal.Delete(pageDetail);
        }

        public PageDetail GetByPageDetailID(int id)
        {
            return _pageDetailDal.Find(x=>x.PageDetail_ID==id);
        }

        public PageDetail GetByPageID(int id)
        {
            return _pageDetailDal.Find(x => x.PageID == id);
        }

        public List<PageDetail> GetList()
        {
            return _pageDetailDal.GetList();
        }

        public void Update(PageDetail pageDetail)
        {
            _pageDetailDal.Update(pageDetail);
        }
    }
}
