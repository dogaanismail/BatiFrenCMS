using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IMenuService
    {
        List<Menu> GetList();
        Menu GetByMenuId(int id);
        void Add(Menu menu);
        void Delete(Menu menu);
        void Update(Menu menu);
    }
}
