using BatiFren.Business.Abstract;
using BatiFren.Business.BusinessResult;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Entities;
using BatiFren.Entities.EntityClasses;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        private IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        GeneralHelper generalhelper = new GeneralHelper();
        Dictionary<string, object> res = new Dictionary<string, object>();

        public ActionResult Login()
        {
            if (Session["login"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["access"] != null)
            {
                model.UserName = Request.Cookies["access"].Values["UserName"];
                model.Password = Request.Cookies["access"].Values["Password"];
                model.RememberMe = Convert.ToBoolean(Request.Cookies["access"].Values["RememberMe"]);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                BusinessResults<User> user = _userService.Login(model);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError(" ", x.Message));
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                user.result.LastLoginDate = DateTime.Now;
                System.Web.HttpContext.Current.Session["EncryptedUserID"] = generalhelper.Encrypt(user.result.UserID.ToString());
                System.Web.HttpContext.Current.Session["UserID"] = user.result.UserID;
                System.Web.HttpContext.Current.Session["RoleID"] = user.result.RoleID;
                System.Web.HttpContext.Current.Session["RoleName"] = user.result.Role.RoleName;
                System.Web.HttpContext.Current.Session["UserName"] = user.result.UserName.ToString();
                System.Web.HttpContext.Current.Session["PhotoPath"] = user.result.PhotoPath;
                System.Web.HttpContext.Current.Session["InsertDate"] = Convert.ToDateTime(user.result.CreatedDate).ToShortDateString();

                _userService.Update(user.result);

                HttpCookie cookie = new HttpCookie("access");
                if (model.RememberMe)
                {
                    cookie.Values.Add("UserName", model.UserName);
                    cookie.Values.Add("Password", model.Password);
                    cookie.Values.Add("RoleID", user.result.RoleID.ToString());
                    cookie.Values.Add("RememberMe", model.RememberMe.ToString());
                    cookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Values.Add("UserName", "");
                    cookie.Values.Add("Password", "");
                    cookie.Values.Add("RoleID", "");
                    cookie.Values.Add("RememberMe", "");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
                Session["login"] = user.result;
                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("login");
            return RedirectToAction("Index", "Dashboard");
        }
    }
}