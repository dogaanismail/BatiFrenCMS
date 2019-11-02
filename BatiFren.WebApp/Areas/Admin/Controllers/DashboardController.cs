using BatiFren.Business.Abstract;
using BatiFren.Business.DependencyResolvers.Ninject;
using System.Linq;
using System.Web.Mvc;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private IPageService _pageService = InstanceFactory.GetInstance<IPageService>();
        private IMessageService _messageService = InstanceFactory.GetInstance<IMessageService>();
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.GetMessages = _messageService.GetAllLazyWithoutID().OrderBy(x => x.InsertDate).ToList();
                ViewBag.Pages = _pageService.GetList();
               
                return View();
            }         
        }
    }
}