using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Admin/Role
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
    }
}