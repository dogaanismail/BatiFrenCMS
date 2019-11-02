using BatiFren.Business.Abstract;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Common.MyExtensionClasses;
using BatiFren.Entities;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        private IPageService _pageService = InstanceFactory.GetInstance<IPageService>();
        private IPageDetailService _detailService = InstanceFactory.GetInstance<IPageDetailService>();
        private IMenuService _menuService = InstanceFactory.GetInstance<IMenuService>();

        #region Index
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
           
        }
        #endregion

        #region PageContent
        public ActionResult EditContent(int id)
        {
            ViewBag.PAGE = _pageService.GetByPageID(id);
            PageDetail detail = _detailService.GetByPageID(id);
            return View(detail);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditContent(PageDetail detail)
        {
            int proId = Convert.ToInt16(Url.RequestContext.RouteData.Values["id"]);

            PageDetail detail2 = _detailService.GetByPageID(proId);
            if (detail2 == null)
            {
                detail.PageID = proId;
                detail.CreatedTime = DateTime.Now;
                HttpUtility.HtmlDecode(detail.Content);
                _detailService.Add(detail);
                return RedirectToAction("EditContent", "Pages", new { id = proId });
            }
            else
            {
                detail.PageID = proId;
                detail.LastModifiedTime = DateTime.Now;
                HttpUtility.HtmlDecode(detail.Content);
                _detailService.Update(detail);
                return RedirectToAction("EditContent", "Pages", new { id = proId });
            }
        }
        #endregion

        #region Page
        public ActionResult AddPage()
        {
            ViewBag.Pages = _pageService.GetList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddPage(Page page)
        {
            string url = page.PageName;
            string newurl = url.TrimSlash();

            page.Url = newurl;
            page.CreatedTime = DateTime.Now;
            _pageService.Add(page);

            ViewBag.Pages = _pageService.GetList();
            return View();
        }

        public ActionResult EditPage(int id)
        {
            return PartialView(_pageService.GetByPageID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPagePost(Page page)
        {
            if (ModelState.IsValid)
            {
                string url = page.PageName;
                string newurl = url.TrimSlash();
                page.Url = newurl;
                page.LastModifiedTime = DateTime.Now;
                _pageService.Update(page);
                return RedirectToAction("AddPage");
            }
            return View();
        }

        public ActionResult DeletePage(int id)
        {
            Page pages = _pageService.GetByPageID(id);
            PageDetail pageDetail = _detailService.GetByPageID(id);
            _detailService.Delete(pageDetail);
            _pageService.Delete(pages);
            ViewBag.Pages = _pageService.GetList();
            return RedirectToAction("AddPage");
        }
        #endregion
    }
}