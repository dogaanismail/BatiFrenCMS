using BatiFren.Business.Abstract;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Common.MyExtensionClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BatiFren.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IPageService _pageService = InstanceFactory.GetInstance<IPageService>();
        private IMenuService _menuService = InstanceFactory.GetInstance<IMenuService>();
        public ActionResult Index(string url)
        {
            if (url == null)
            {
                return Redirect("/Index");
            }

            url = url.TrimSlash();  //extension method     
            var model = _pageService.FindByUrl(url);

            if (model == null || model.IsActive == false)
                return HttpNotFound();

            var pagedetail = model.PageDetails.FirstOrDefault();
            string viewName = "";
            if (model.PreDefinedID == "ExamplePage")
            {
                viewName = "ExamplePage";
                ViewBag.Title = pagedetail.Title.ToString();
                ViewBag.Content = pagedetail.Content;
                ViewBag.Keywords = pagedetail.SeoKeywords.ToString();
                ViewBag.Description = pagedetail.SeoDescription.ToString();       
                return View(viewName, model);
            }
            else if (model.PreDefinedID == "hakkimizda")
            {
                viewName = "hakkimizda";
                ViewBag.Title = pagedetail.Title.ToString();
                ViewBag.Content = pagedetail.Content;
                ViewBag.Keywords = pagedetail.SeoKeywords.ToString();
                ViewBag.Description = pagedetail.SeoDescription.ToString();
                return View(viewName, model);
            }
            return View(viewName, model);
        }

        public ActionResult GetMenu()
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
                      url= l.Url,
                      Children = GetChildren(locations, l.MenuID)
                  }).ToList();

            ViewBag.menusList = menu;
            return PartialView();
        }

        private List<Entities.EntityClasses.Menu> GetChildren(List<Entities.Menu> locations, int menuID)
        {
            return locations.Where(l => l.ParentID == menuID).OrderBy(l => l.OrderNumber).
               Select(l => new Entities.EntityClasses.Menu
               {
                   id = l.MenuID,
                   text = l.MenuName,
                   type = l.Type,
                   url=l.Url,
                   Children = GetChildren(locations, l.MenuID)
               }).ToList();
        }


    }
}
