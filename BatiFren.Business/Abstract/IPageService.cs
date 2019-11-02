using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IPageService
    {
        List<Page> GetList();
        void Add(Page page);
        void Delete(Page page);
        void Update(Page page);
        Page GetByPageID(int id);
        Page FindByUrl(string url);
    }
}
