using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class MenuManager : IMenuService
    {
        private IMenuDal _menuDal;
        public MenuManager(IMenuDal menuDal)
        {
            _menuDal = menuDal;
        }
        public void Add(Menu menu)
        {
            _menuDal.Add(menu);
        }

        public void Delete(Menu menu)
        {
            _menuDal.Delete(menu);
        }

        public Menu GetByMenuId(int id)
        {
            return _menuDal.Find(x => x.MenuID == id);
        }

        public List<Menu> GetList()
        {
            return _menuDal.GetList();
        }

        public void Update(Menu menu)
        {
            _menuDal.Update(menu);
        }
    }
}
