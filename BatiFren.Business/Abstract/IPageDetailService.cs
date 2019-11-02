using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IPageDetailService
    {
        List<PageDetail> GetList();
        PageDetail GetByPageDetailID(int id);
        PageDetail GetByPageID(int id);
        void Add(PageDetail pageDetail);
        void Delete(PageDetail pageDetail);
        void Update(PageDetail pageDetail);
    }
}
