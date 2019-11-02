using BatiFren.Business.Abstract;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        private IMenuService _menuService = InstanceFactory.GetInstance<IMenuService>();
        private IPageService _pageService = InstanceFactory.GetInstance<IPageService>();
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {          
            List<Entities.Menu> locations;
            List<Entities.EntityClasses.Menu> menu;

            locations = _menuService.GetList();
            menu = locations.Where(l => l.ParentID == null).OrderBy(l => l.OrderNumber).
                  Select(l => new Entities.EntityClasses.Menu
                  {
                      id = l.MenuID,
                      text = l.MenuName,
                      type = l.Type,
                      Children = GetChildren(locations, l.MenuID)
                  }).ToList();

            ViewBag.menusList = menu;
            ViewBag.latestPages = _pageService.GetList().OrderBy(x => x.CreatedTime).Take(5);
            ViewBag.Pages = _pageService.GetList();
            return View();
            }
        }     
        private List<Entities.EntityClasses.Menu> GetChildren(List<Entities.Menu> locations, int menuID)
        {
            return locations.Where(l => l.ParentID == menuID).OrderBy(l => l.OrderNumber).
               Select(l => new Entities.EntityClasses.Menu
               {
                   id = l.MenuID,
                   text = l.MenuName,
                   type = l.Type,
                   Children = GetChildren(locations, l.MenuID)
               }).ToList();
        }

        [HttpPost]
        public JsonResult ChangeNodePosition(int currentNode, int? parentNode, int position)
        {
            using (BatiFrenDBEntities db = new BatiFrenDBEntities())
            {
                var location = db.Menus.First(x => x.MenuID == currentNode);
                var newSiblingsBelow = db.Menus.Where(l => l.ParentID == parentNode && l.OrderNumber >= position);
                foreach (var sibling in newSiblingsBelow)
                {
                    sibling.OrderNumber = sibling.OrderNumber + 1;
                }

                var oldSiblingsBelow = db.Menus.Where(l => l.ParentID == parentNode && l.OrderNumber > location.OrderNumber);
                foreach (var sibling in oldSiblingsBelow)
                {
                    sibling.OrderNumber = sibling.OrderNumber - 1;
                }

                location.ParentID = parentNode;
                location.OrderNumber = position;
                db.SaveChanges();
            }
            return this.Json(true);
        }
        public ActionResult AddMenu(List<int> data)
        {
            foreach (var item in data)
            {
                int menu = Convert.ToInt32(_menuService.GetList().Where(x => x.ParentID == null).Select(x => x.OrderNumber).Max());
                Page pages = _pageService.GetByPageID(item);
                Menu menuAdding = new Menu();
                if (menu != null)
                    menuAdding.OrderNumber = menu + 1;
                else
                    menuAdding.OrderNumber = 0;
                menuAdding.ParentID = null;
                menuAdding.Url = pages.Url;
                menuAdding.Type = "Page";
                menuAdding.MenuName = pages.PageName;
                _menuService.Add(menuAdding);
            }
            List<Menu> locations;
            List<Entities.EntityClasses.Menu> menuNew;

            locations = _menuService.GetList();
            menuNew = locations.Where(l => l.ParentID == null).OrderBy(l => l.OrderNumber).
                  Select(l => new Entities.EntityClasses.Menu
                  {
                      id = l.MenuID,
                      text = l.MenuName,
                      type = l.Type,
                      Children = GetChildren(locations, l.MenuID)
                  }).ToList();

            ViewBag.menusList = menuNew;
            ViewBag.latestPages = _pageService.GetList().OrderBy(x => x.CreatedTime).Take(5);
            ViewBag.Pages = _pageService.GetList();
            return Json(new { result = "Redirect", url = Url.Action("Index", "Menu") });
        }
        public ActionResult AddLink(string url, string text)
        {
            int maxOrder = Convert.ToInt32(_menuService.GetList().Where(x => x.ParentID == null).Select(x => x.OrderNumber).Max());
            Menu newMenu = new Menu();

            if (maxOrder != null)
                newMenu.OrderNumber = maxOrder + 1;
            else
                newMenu.OrderNumber = 0;

            newMenu.ParentID = null;
            newMenu.MenuName = text.ToString();
            newMenu.Type = "Special Link";
            newMenu.Url = url.ToString();
            _menuService.Add(newMenu);

            List<Menu> locations;
            List<Entities.EntityClasses.Menu> menuNew;

            locations = _menuService.GetList();
            menuNew = locations.Where(l => l.ParentID == null).OrderBy(l => l.OrderNumber).
                  Select(l => new Entities.EntityClasses.Menu
                  {
                      id = l.MenuID,
                      text = l.MenuName,
                      type = l.Type,
                      Children = GetChildren(locations, l.MenuID)
                  }).ToList();

            ViewBag.menusList = menuNew;
            ViewBag.latestPages = _pageService.GetList().OrderBy(x => x.CreatedTime).Take(5);
            ViewBag.Pages = _pageService.GetList();
            return RedirectToAction("Index","Menu");
        }

        public ActionResult PreviewMenu()
        {
            List<Menu> locations;
            List<Entities.EntityClasses.Menu> menu;

            locations = _menuService.GetList();
            menu = locations.Where(l => l.ParentID == null).OrderBy(l => l.OrderNumber).
                  Select(l => new Entities.EntityClasses.Menu
                  {
                      id = l.MenuID,
                      text = l.MenuName,
                      type = l.Type,
                      Children = GetChildren(locations, l.MenuID)
                  }).ToList();

            ViewBag.menusList = menu;
            return PartialView();
        }
    }
}